namespace Orc.Csv;

using System;
using Catel.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class DynamicTypeConverter<T> : ITypeConverter
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly Func<string?, IReaderRow, MemberMapData, T?> _convertFromString;
    private readonly Func<object?, IWriterRow, MemberMapData, string?> _convertToString;
    private readonly string? _defaultValue;

    public DynamicTypeConverter(Func<string?, IReaderRow?, MemberMapData, T?> convertFromString,
        Func<object?, IWriterRow, MemberMapData, string?> convertToString,
        string? defaultValue = null)
    {
        ArgumentNullException.ThrowIfNull(convertFromString);
        ArgumentNullException.ThrowIfNull(convertToString);

        _convertFromString = convertFromString;
        _convertToString = convertToString;
        _defaultValue = defaultValue;
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is null)
        {
            return _defaultValue;
        }

        return _convertToString(value, row, memberMapData);
    }

    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (!string.IsNullOrWhiteSpace(_defaultValue) && string.IsNullOrWhiteSpace(text))
        {
            text = _defaultValue;
        }

        return _convertFromString(text, row, memberMapData);
    }
}
