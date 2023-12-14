namespace Orc.Csv;

using System.Collections.Generic;
using System.Linq;
using Catel;
using CsvHelper;
using CsvHelper.Configuration;

public class BooleanConverter : TypeConverterBase
{
    private readonly List<string> _trueValuesList = new();

    private string[]? _trueValues;

    public BooleanConverter()
    {
        _trueValuesList.AddRange(new [] { "yes", "1", "on", "true", "y", "t" });
    }

    public BooleanConverter(string[]? trueValues)
        : this()
    {
        if (trueValues is null)
        {
            return;
        }

        // Replace the list
        _trueValuesList.Clear();
        _trueValuesList.AddRange(trueValues);
    }

    public BooleanConverter AddTrueValues(params string[] values)
    {
        if (!values.Any())
        {
            return this;
        }

        _trueValuesList.AddRange(values);
        _trueValues = null;

        return this;
    }

    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData? memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        text = text.Trim();

        _trueValues ??= _trueValuesList.ToArray();

        return text.EqualsAnyIgnoreCase(_trueValues);
        // Note: no need to check for false values (default value is false)
    }
}
