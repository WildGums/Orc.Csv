namespace Orc.CsvHelper.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    public interface ICsvReaderService
    {
        IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap;

        IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null);

        IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap map, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null);

        CsvReader CreateReader(string csvFilePath, Type classMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null);

        CsvReader CreateReader(string csvFilePath, CsvClassMap classMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null);
    }
}