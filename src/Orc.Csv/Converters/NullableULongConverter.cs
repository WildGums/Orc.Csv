namespace Orc.Csv;

using System;
using CsvHelper;

public class NullableULongConverter : NullableTypeConverterBase<ulong?>
{
    protected override ulong? ConvertStringToActualType(IReaderRow row, string text)
    {
        var value = Convert.ToUInt32(text, GetCultureInfo(row));
        return value;
    }
}
