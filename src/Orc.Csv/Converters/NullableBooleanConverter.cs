namespace Orc.Csv;

using System.Collections.Generic;
using System.Linq;
using Catel;
using CsvHelper;

public class NullableBooleanConverter : NullableTypeConverterBase<bool?>
{
    private readonly List<string> _trueValuesList = new();
    private readonly List<string> _falseValuesList = new();

    private string[]? _trueValues;
    private string[]? _falseValues;

    public NullableBooleanConverter()
    {
        _trueValuesList.AddRange(new[] { "yes", "1", "on", "true" });
        _falseValuesList.AddRange(new[] { "no", "0", "off", "false" });
    }

    public NullableBooleanConverter(string[]? trueValues, string[]? falseValues)
        : this()
    {
        if (trueValues is not null)
        {
            // Replace the list
            _trueValuesList.Clear();
            _trueValuesList.AddRange(trueValues);
        }

        if (falseValues is not null)
        {
            // Replace the list
            _falseValuesList.Clear();
            _falseValuesList.AddRange(falseValues);
        }
    }

    public NullableBooleanConverter AddTrueValues(params string[] values)
    {
        if (values.Any())
        {
            _trueValuesList.AddRange(values);
            _trueValues = null;
        }

        return this;
    }

    public NullableBooleanConverter AddFalseValues(params string[] values)
    {
        if (values.Any())
        {
            _falseValuesList.AddRange(values);
            _falseValues = null;
        }

        return this;
    }

    protected override bool? ConvertStringToActualType(IReaderRow? row, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        text = text.Trim();

        _trueValues ??= _trueValuesList.ToArray();

        if (text.EqualsAnyIgnoreCase(_trueValues))
        {
            return true;
        }

        _falseValues ??= _falseValuesList.ToArray();

        return false;
    }
}
