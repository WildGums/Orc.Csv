namespace Orc.Csv
{
    using CsvHelper.TypeConversion;
    using System;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class NullableStringConverter : NullableTypeConverterBase<string>
    {
        protected override string ConvertStringToActualType(IReaderRow row, string text)
        {
            return text;
        }
    }
}
