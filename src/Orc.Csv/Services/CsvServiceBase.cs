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
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    public abstract class CsvServiceBase
    {
        // Non-static so we can get derived type, services will not have many instances
        // anyway
        private readonly ILog Log;

        protected CsvServiceBase()
        {
            Log = LogManager.GetLogger(GetType());
        }

        public virtual Configuration CreateDefaultConfiguration(ICsvContext csvContext)
        {
            var configuration = new Configuration
            {
                CultureInfo = csvContext?.Culture ?? CsvEnvironment.DefaultCultureInfo,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
            };

            return configuration;
        }

        protected virtual Configuration EnsureCorrectConfiguration(Configuration configuration, ICsvContext csvContext)
        {
            configuration = configuration ?? CreateDefaultConfiguration(csvContext);

            // Always create a new config object so we can wrap it
            var finalConfiguration = new Configuration
            {
                AllowComments = configuration.AllowComments,
                BufferSize = configuration.BufferSize,
                BuildRequiredQuoteChars = configuration.BuildRequiredQuoteChars,
                Comment = configuration.Comment,
                CountBytes = configuration.CountBytes,
                CultureInfo = csvContext.Culture ?? configuration.CultureInfo,
                Delimiter = configuration.Delimiter,
                DetectColumnCountChanges = configuration.DetectColumnCountChanges,
                Encoding = configuration.Encoding,
                GetConstructor = configuration.GetConstructor,
                HasHeaderRecord = configuration.HasHeaderRecord,
                HeaderValidated = configuration.HeaderValidated,
                IgnoreBlankLines = configuration.IgnoreBlankLines,
                IgnoreQuotes = configuration.IgnoreQuotes,
                IgnoreReferences = configuration.IgnoreReferences,
                IncludePrivateMembers = configuration.IncludePrivateMembers,
                InjectionCharacters = configuration.InjectionCharacters,
                InjectionEscapeCharacter = configuration.InjectionEscapeCharacter,
                MemberTypes = configuration.MemberTypes,
                PrepareHeaderForMatch = configuration.PrepareHeaderForMatch,
                Quote = configuration.Quote,
                QuoteAllFields = configuration.QuoteAllFields,
                QuoteNoFields = configuration.QuoteNoFields,
                ReferenceHeaderPrefix = configuration.ReferenceHeaderPrefix,
                SanitizeForInjection = configuration.SanitizeForInjection,
                ShouldSkipRecord = configuration.ShouldSkipRecord,
                ShouldUseConstructorParameters = configuration.ShouldUseConstructorParameters,
                TrimOptions = configuration.TrimOptions,
                TypeConverterCache = configuration.TypeConverterCache,
                TypeConverterOptionsCache = configuration.TypeConverterOptionsCache,
                UseNewObjectForNullReferenceMembers = configuration.UseNewObjectForNullReferenceMembers
            };

            // Note: configuration.Maps can be ignored

            finalConfiguration.BadDataFound = (context) =>
            {
                Log.Warning($"Found bad data, row '{context.Row}', char position '{context.CharPosition}', field '{context.Field}'");

                var handler = configuration.BadDataFound;
                if (handler != null)
                {
                    handler(context);
                }
            };

            finalConfiguration.HeaderValidated = (isValid, headers, index, context) =>
            {
                if (!isValid)
                {
                    var headerNames = string.Join(", ", headers);

                    Log.Warning($"Header matching '{headerNames}' names at index '{index}' was not found");
                }

                var handler = configuration.HeaderValidated;
                if (handler != null)
                {
                    handler(isValid, headers, index, context);
                }
            };

            finalConfiguration.MissingFieldFound = (fields, position, context) =>
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
                if (handler != null)
                {
                    handler(fields, position, context);
                }
            };

            finalConfiguration.ReadingExceptionOccurred = (ex) =>
            {
                var readingContext = ex.ReadingContext;

                // We always read from a csv file so we know we have a file stream
                var fileName = string.Empty;

                if (ex.ReadingContext.Reader is StreamReader streamReader)
                {
                    if (streamReader.BaseStream is FileStream fileStream)
                    {
                        fileName = fileStream.Name;
                    }
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
                        if (typeConverterException.MemberMapData.IsNameSet)
                        {
                            columnName = typeConverterException.MemberMapData.Names.FirstOrDefault();
                        }
                        else
                        {
                            columnName = $"idx: {typeConverterException.MemberMapData.Index}";
                        }

                        messageBuilder.Append($", content '{typeConverterException.Text}'");

                        var propertyName = typeConverterException.MemberMapData.Member.Name;

                        messageBuilder.Append($", property '{propertyName}'");
                    }

                    //if (ex is BadDataException badDataException)
                    //{
                    //}

                    //if (ex is ParserException parserException)
                    //{
                    //}

                    //if (ex is ReaderException readerException)
                    //{
                    //}

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
                if (handler != null)
                {
                    handler(ex);
                }
            };

            if (csvContext.ClassMap != null)
            {
                finalConfiguration.RegisterClassMap(csvContext.ClassMap);
            }

            return finalConfiguration;
        }
    }
}
