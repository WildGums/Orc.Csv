namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableUShortConverter : NullableTypeConverterBase<ushort?>
    {
        protected override ushort? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToUInt16(text, GetCultureInfo(row));
            return value;
        }
    }
}
