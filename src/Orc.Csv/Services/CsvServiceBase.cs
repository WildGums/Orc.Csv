// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvServiceBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    public abstract class CsvServiceBase
    {
        // Non-static so we can get derived type, services will not have many instances
        // anyway
#pragma warning disable IDE1006 // Naming Styles
        private readonly ILog Log;
#pragma warning restore IDE1006 // Naming Styles

        protected CsvServiceBase()
        {
            Log = LogManager.GetLogger(GetType());
        }

        public virtual CsvConfiguration CreateDefaultConfiguration(ICsvContext csvContext)
        {
            var configuration = new CsvConfiguration(csvContext?.Culture ?? CsvEnvironment.DefaultCultureInfo)
            {
                Delimiter = ",",
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
            };

            return configuration;
        }

        protected virtual CsvConfiguration EnsureCorrectConfiguration(CsvConfiguration configuration, ICsvContext csvContext)
        {
            configuration = configuration ?? CreateDefaultConfiguration(csvContext);

            // Always create a new config object so we can wrap it
            var finalConfiguration = new CsvConfiguration(csvContext.Culture ?? configuration.CultureInfo)
            {
                AllowComments = configuration.AllowComments,
                BufferSize = configuration.BufferSize,
                Comment = configuration.Comment,
                CountBytes = configuration.CountBytes,
                Delimiter = configuration.Delimiter,
                DetectColumnCountChanges = configuration.DetectColumnCountChanges,
                DynamicPropertySort = configuration.DynamicPropertySort,
                Encoding = configuration.Encoding,
                Escape = configuration.Escape,
                GetConstructor = configuration.GetConstructor,
                HasHeaderRecord = configuration.HasHeaderRecord,
                HeaderValidated = configuration.HeaderValidated,
                IgnoreBlankLines = configuration.IgnoreBlankLines,
                IgnoreQuotes = configuration.IgnoreQuotes,
                IgnoreReferences = configuration.IgnoreReferences,
                IncludePrivateMembers = configuration.IncludePrivateMembers,
                InjectionCharacters = configuration.InjectionCharacters,
                InjectionEscapeCharacter = configuration.InjectionEscapeCharacter,
                LineBreakInQuotedFieldIsBadData = configuration.LineBreakInQuotedFieldIsBadData,
                //Maps = configuration.Maps,
                MemberTypes = configuration.MemberTypes,
                PrepareHeaderForMatch = configuration.PrepareHeaderForMatch,
                Quote = configuration.Quote,
                //QuoteString = configuration.QuoteString,
                ReferenceHeaderPrefix = configuration.ReferenceHeaderPrefix,
                SanitizeForInjection = configuration.SanitizeForInjection,
                ShouldQuote = configuration.ShouldQuote,
                ShouldSkipRecord = configuration.ShouldSkipRecord,
                ShouldUseConstructorParameters = configuration.ShouldUseConstructorParameters,
                TrimOptions = configuration.TrimOptions,
                TypeConverterCache = configuration.TypeConverterCache,
                TypeConverterOptionsCache = configuration.TypeConverterOptionsCache,
                UseNewObjectForNullReferenceMembers = configuration.UseNewObjectForNullReferenceMembers
            };

            // Note: configuration.Maps can be ignored

            finalConfiguration.BadDataFound = (context) => HandleBadDataFound(context, configuration); 

            finalConfiguration.HeaderValidated = (invalidHeaders, context) => HandleHeaderValidated(invalidHeaders, context, configuration);

            finalConfiguration.MissingFieldFound = (fields, position, context) => HandleMissingFieldFound(fields, position, context, configuration, csvContext);

            finalConfiguration.ReadingExceptionOccurred = (ex) => HandleReadingException(ex, configuration);

            if (csvContext.ClassMap != null)
            {
                finalConfiguration.RegisterClassMap(csvContext.ClassMap);
            }

            return finalConfiguration;
        }

        private void HandleBadDataFound(ReadingContext context, CsvConfiguration configuration)
        {
            Log.Warning($"Found bad data, row '{context.Row}', char position '{context.CharPosition}', field '{context.Field}'");

            var handler = configuration.BadDataFound;
            handler?.Invoke(context);
        }

        private void HandleHeaderValidated(InvalidHeader[] invalidHeaders, ReadingContext context, CsvConfiguration configuration)
        {
            foreach (var invalidHeader in invalidHeaders)
            {
                var headerNames = string.Join(", ", invalidHeader.Names);

                Log.Warning($"Header matching '{headerNames}' names at index '{invalidHeader.Index}' was not found");
            }

            var handler = configuration.HeaderValidated;
            handler?.Invoke(invalidHeaders, context);
        }

        private void HandleMissingFieldFound(string[] fields, int position, ReadingContext context, CsvConfiguration configuration, ICsvContext csvContext)
        {
            // Don't log when fields are null, special case for which we don't want to pollute the logs
            if (fields != null)
            {
                var ignoreWarning = true;

                // This could be a *mapped* field that is not part of the file (thus should not have a header record entry either)
                var headerRecord = context.HeaderRecord;
                if (headerRecord != null)
                {
                    foreach (var field in fields)
                    {
                        if (headerRecord.Contains(field))
                        {
                            ignoreWarning = false;
                        }
                        else if (context.Row <= 2)
                        {
                            var classMap = csvContext.ClassMap?.GetType()?.Name ?? "no-class-map";

                            Log.Debug("Found field '{0}' defined in class map '{1}', but it's not defined in the actual file", field, classMap);
                        }
                    }
                }

                if (!ignoreWarning)
                {
                    Log.Warning("Found '{0}' missing fields at row '{1}', char position '{1}': '{2}'", fields.Length, context.Row, position, string.Join(",", fields));
                }
            }

            var handler = configuration.MissingFieldFound;
            handler?.Invoke(fields, position, context);
        }

        private bool HandleReadingException(CsvHelperException ex, CsvConfiguration configuration)
        {
            var readingContext = ex.ReadingContext;

            // We always read from a csv file so we know we have a file stream
            var fileName = string.Empty;

            if (ex.ReadingContext.Reader is StreamReader streamReader
                && streamReader.BaseStream is FileStream fileStream)
            {
                fileName = fileStream.Name;
            }

            var messageBuilder = new StringBuilder();
            messageBuilder.Append("An exception occurred during reading");

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                messageBuilder.Append($", file: '{fileName}'");
            }

            if (readingContext != null)
            {
                messageBuilder.Append($", row '{readingContext.Row}'");

                var columnName = readingContext.Field;

                if (ex is TypeConverterException typeConverterException)
                {
                    columnName = typeConverterException.MemberMapData.IsNameSet
                        ? typeConverterException.MemberMapData.Names.FirstOrDefault()
                        : $"idx: {typeConverterException.MemberMapData.Index}";

                    messageBuilder.Append($", content '{typeConverterException.Text}'");

                    var propertyName = typeConverterException.MemberMapData.Member.Name;

                    messageBuilder.Append($", property '{propertyName}'");
                }

                messageBuilder.Append($", column '{columnName}'");
            }

            var writingContext = ex.WritingContext;
            if (writingContext != null)
            {
                messageBuilder.Append($", row '{writingContext.Row}'");
            }

            messageBuilder.Append($", message: '{ex.Message}'");

            var message = messageBuilder.ToString();
            Log.Error(message);

            var handler = configuration.ReadingExceptionOccurred;
            handler?.Invoke(ex);

            return false;
        }
    }
}
