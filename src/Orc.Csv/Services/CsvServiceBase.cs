namespace Orc.Csv;

using System.IO;
using System.Linq;
using Catel.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;

public abstract class CsvServiceBase
{
    private readonly ILogger _logger;

    protected CsvServiceBase(ILogger logger)
    {
        _logger = logger;
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

    protected virtual CsvConfiguration EnsureCorrectConfiguration(CsvConfiguration? configuration, ICsvContext csvContext)
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
            ShouldQuote = configuration.ShouldQuote,
            ShouldSkipRecord = configuration.ShouldSkipRecord,
            ShouldUseConstructorParameters = configuration.ShouldUseConstructorParameters,
            TrimOptions = configuration.TrimOptions,
            UseNewObjectForNullReferenceMembers = configuration.UseNewObjectForNullReferenceMembers
        };

        // Clear specific handlers we want to ignore
        configuration.BadDataFound -= ConfigurationFunctions.BadDataFound;

        // Note: configuration.Maps can be ignored

        finalConfiguration.BadDataFound = args => HandleBadDataFound(args, configuration);
        finalConfiguration.HeaderValidated = args => HandleHeaderValidated(args, configuration);
        finalConfiguration.MissingFieldFound = args => HandleMissingFieldFound(args, configuration, csvContext);
        finalConfiguration.ReadingExceptionOccurred = ex => HandleReadingException(ex, configuration);

        return finalConfiguration;
    }

    private void HandleBadDataFound(BadDataFoundArgs args, CsvConfiguration configuration)
    {
        _logger.LogWarning("Found bad data, row '{Row}', char position '{CharPosition}', field '{Field}'", args.Context.Parser?.Row ?? 0, args.Context.Parser?.CharCount ?? 0, args.Field);

        var handler = configuration.BadDataFound;
        handler?.Invoke(args);
    }

    private void HandleHeaderValidated(HeaderValidatedArgs args, CsvConfiguration configuration)
    {
        foreach (var invalidHeader in args.InvalidHeaders)
        {
            var headerNames = string.Join(", ", invalidHeader.Names);

            _logger.LogWarning("Header matching '{HeaderNames}' names at index '{HeaderIndex}' was not found", headerNames, invalidHeader.Index);
        }

        var handler = configuration.HeaderValidated;
        handler?.Invoke(args);
    }

    private void HandleMissingFieldFound(MissingFieldFoundArgs args, CsvConfiguration configuration, ICsvContext csvContext)
    {
        var context = args.Context;
        var fields = args.HeaderNames;

        var reader = context.Reader;
        if (reader is null)
        {
            return;
        }

        // Don't log when fields are null, special case for which we don't want to pollute the logs
        if (fields is not null)
        {
            var ignoreWarning = true;

            // This could be a *mapped* field that is not part of the file (thus should not have a header record entry either)
            var headerRecord = reader.HeaderRecord;
            if (headerRecord is not null)
            {
                foreach (var field in fields)
                {
                    if (headerRecord.Contains(field))
                    {
                        ignoreWarning = false;
                    }
                    else if ((context.Parser?.Row ?? 0) <= 2)
                    {
                        var classMap = csvContext.ClassMap?.GetType().Name ?? "no-class-map";

                        _logger.LogDebugIfAttached($"Found field '{field}' defined in class map '{classMap}', but it's not defined in the actual file");
                    }
                }
            }

            if (!ignoreWarning)
            {
                _logger.LogWarning("Found '{FieldCount}' missing fields at row '{Row}', char position '{CharPosition}': '{Fields}'", 
                    fields.Length, context.Parser?.Row, context.Parser?.CharCount, string.Join(",", fields));
            }
        }

        var handler = configuration.MissingFieldFound;
        handler?.Invoke(args);
    }

    private bool HandleReadingException(ReadingExceptionOccurredArgs args, CsvConfiguration configuration)
    {
        var ex = args.Exception;
        var readingContext = ex.Context?.Reader;

        // We always read from a csv file so we know we have a file stream
        string? fileName = null;

        if (readingContext is StreamReader { BaseStream: FileStream fileStream })
        {
            fileName = fileStream.Name;
        }

        var row = ex.Context?.Parser?.Row;
        string? columnName = null;
        string? content = null;
        string? propertyName = null;

        if (readingContext is not null)
        {
            columnName = ex.Context?.Reader?.HeaderRecord?[ex.Context.Reader.CurrentIndex] ?? "unknown";

            if (ex is TypeConverterException typeConverterException)
            {
                columnName = typeConverterException.MemberMapData.IsNameSet
                    ? typeConverterException.MemberMapData.Names.FirstOrDefault()
                    : $"idx: {typeConverterException.MemberMapData.Index}";

                content = typeConverterException.Text;
                propertyName = typeConverterException.MemberMapData.Member?.Name;
            }
        }

        var writingRow = ex.Context?.Writer?.Row;

        _logger.LogWarning("An exception occurred during reading, file: '{FileName}', row '{Row}', content '{Content}', property '{PropertyName}', column '{ColumnName}', writing row '{WritingRow}', message: '{Message}'",
            fileName, row, content, propertyName, columnName, writingRow, ex.Message);

        var handler = configuration.ReadingExceptionOccurred;
        handler?.Invoke(args);

        return true;
    }
}
