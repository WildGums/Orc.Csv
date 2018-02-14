// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvServiceBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Globalization;
    using Catel;
    using Catel.Logging;
    using CsvHelper.Configuration;

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
                CultureInfo = csvContext?.CultureInfo ?? CsvEnvironment.DefaultCultureInfo,
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
                CultureInfo = csvContext.CultureInfo ?? configuration.CultureInfo,
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
                Log.Warning("Found '{0}' missing fields: '{1}'", fields.Length, string.Join(",", fields));

                var handler = configuration.MissingFieldFound;
                if (handler != null)
                {
                    handler(fields, position, context);
                }
            };

            finalConfiguration.ReadingExceptionOccurred = (ex) =>
            {
                var message = "An exception occurred";

                var readingContext = ex.ReadingContext;
                if (readingContext != null)
                {
                    message += $", row '{readingContext.Row}', char position '{readingContext.CharPosition}', field '{readingContext.Field}'";
                }

                var writingContext = ex.WritingContext;
                if (writingContext != null)
                {
                    message += $", row '{writingContext.Row}'";
                }

                message += $", message: '{ex.Message}'";

                Log.Warning(message);

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