namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableDoubleConverter : NullableTypeConverterBase<double?>
    {
        protected override double? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToDouble(text, GetCultureInfo(row));
            if (double.IsNaN(value))
            {
                return null;
            }

            return value;
        }
    }
}
