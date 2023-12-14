namespace Orc.Csv;

using System;
using CsvHelper;

public class NullableUIntConverter : NullableTypeConverterBase<uint?>
{
    protected override uint? ConvertStringToActualType(IReaderRow row, string text)
    {
        var value = Convert.ToUInt32(text, GetCultureInfo(row));
        return value;
    }
}
