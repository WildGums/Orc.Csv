namespace Orc.Csv;

using System;
using CsvHelper;

public class NullableDecimalConverter : NullableTypeConverterBase<decimal?>
{
    protected override decimal? ConvertStringToActualType(IReaderRow row, string text)
    {
        var value = Convert.ToDecimal(text, GetCultureInfo(row));
        return value;
    }
}
