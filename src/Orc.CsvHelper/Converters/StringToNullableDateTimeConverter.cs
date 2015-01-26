namespace Orc.Csv
{
    using System;

    public class StringToNullableDateTimeConverter : TypeConverter<DateTime?>
    {
        public StringToNullableDateTimeConverter()
            : base(ConvertFromString)
        {
        }

        public StringToNullableDateTimeConverter(string defaultInput)
            : base(ConvertFromString, defaultInput)
        {
            throw new NotImplementedException();
        }

        private static DateTime? ConvertFromString(string s1, string s2)
        {
            throw new NotImplementedException();
        }

        private static DateTime? ConvertFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var dateTime = Convert.ToDateTime(input);
            if (dateTime == CsvEnvironment.ExcelNullDate)
            {
                return null;
            }

            return dateTime;
        }

    }
}