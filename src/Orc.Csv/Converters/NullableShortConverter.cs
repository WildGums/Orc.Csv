namespace Orc.Csv;

using System;
using CsvHelper;

public class NullableShortConverter : NullableTypeConverterBase<short?>
{
    protected override short? ConvertStringToActualType(IReaderRow row, string text)
    {
        var value = Convert.ToInt16(text, GetCultureInfo(row));
        return value;
    }
}
