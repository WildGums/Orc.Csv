namespace Orc.Csv;

using System;
using Catel;
using CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// Generic enum converter which can be used with CsvHelper
/// </summary>
/// <typeparam name="T">Type of enum</typeparam>
public class EnumConverter<T> : TypeConverterBase
    where T : struct, IComparable, IFormattable
{
    public EnumConverter()
        : this(default(T))
    {
    }
    public EnumConverter(T defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public T DefaultValue { get; set; }

    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return DefaultValue;
        }

        return Enum<T>.TryParse(text, true, out T result) ? result : DefaultValue;
    }
}
