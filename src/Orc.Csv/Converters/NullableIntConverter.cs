namespace Orc.Csv;

using System;
using CsvHelper;

public class NullableIntConverter : NullableTypeConverterBase<int?>
{
    protected override int? ConvertStringToActualType(IReaderRow row, string text)
    {
        var value = Convert.ToInt32(text, GetCultureInfo(row));
        return value;
    }
}
