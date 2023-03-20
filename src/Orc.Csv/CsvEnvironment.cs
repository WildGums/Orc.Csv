namespace Orc.Csv;

using System;
using System.Globalization;

public static class CsvEnvironment
{
    public static readonly DateTime ExcelNullDate = new(1900, 1, 1, 0, 0, 0);

    public static readonly CultureInfo DefaultCultureInfo = new("en-AU");
}
