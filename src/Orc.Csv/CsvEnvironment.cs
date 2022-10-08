namespace Orc.Csv
{
    using System;
    using System.Globalization;

    public static class CsvEnvironment
    {
        public static readonly DateTime ExcelNullDate = new DateTime(1900, 1, 1, 0, 0, 0);

        public static readonly CultureInfo DefaultCultureInfo = new CultureInfo("en-AU");
    }
}
