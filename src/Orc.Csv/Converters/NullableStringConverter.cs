namespace Orc.Csv
{
    using CsvHelper;

    public class NullableStringConverter : NullableTypeConverterBase<string>
    {
        protected override string? ConvertStringToActualType(IReaderRow row, string text)
        {
            return text;
        }
    }
}
