namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableDateTimeConverter : NullableTypeConverterBase<DateTime?>
    {
        protected override DateTime? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToDateTime(text, GetCultureInfo(row));
            if (value == CsvEnvironment.ExcelNullDate)
            {
                return null;
            }

            return value;
        }
    }
}
