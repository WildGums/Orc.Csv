namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableLongConverter : NullableTypeConverterBase<long?>
    {
        protected override long? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToInt64(text, GetCultureInfo(row));
            return value;
        }
    }
}
