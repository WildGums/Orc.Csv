// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper.Configuration;

    public static class ICsvWriterServiceExtensions
    {
        #region Methods
        public static void WriteCsv<TRecord>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvWriterService);

            var typeFactory = csvWriterService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMap);
            csvWriterService.WriteCsv(records, csvFilePath, typeof(TRecord), csvMapInstance, csvConfiguration, throwOnError, cultureInfo);
        }

        public static void WriteCsv<TRecord>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, StreamWriter streamWriter, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvWriterService);

            var typeFactory = csvWriterService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMap);
            csvWriterService.WriteCsv(records, streamWriter, typeof(TRecord), csvMapInstance, csvConfiguration, throwOnError, cultureInfo);
        }

        public static void WriteCsv<TRecord>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvWriterService);

            csvWriterService.WriteCsv(records, csvFilePath, typeof(TRecord), csvMap, csvConfiguration, throwOnError, cultureInfo);
        }

        public static void WriteCsv<TRecord, TMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap
        {
            Argument.IsNotNull(() => csvWriterService);

            csvWriterService.WriteCsv(records, csvFilePath, typeof(TMap), csvConfiguration, throwOnError, cultureInfo);
        }

        public static Task WriteCsvAsync<TRecord>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvWriterService);

            var typeFactory = csvWriterService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMap);
            return csvWriterService.WriteCsvAsync(records, csvFilePath, typeof(TRecord), csvMapInstance, csvConfiguration, throwOnError, cultureInfo);
        }

        public static Task WriteCsvAsync<TRecord, TMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap
        {
            Argument.IsNotNull(() => csvWriterService);

            return csvWriterService.WriteCsvAsync(records, csvFilePath, typeof(TMap), csvConfiguration, throwOnError, cultureInfo);
        }

        public static Task WriteCsvAsync<TRecord>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvWriterService);

            return csvWriterService.WriteCsvAsync(records, csvFilePath, typeof(TRecord), csvMap, csvConfiguration, throwOnError, cultureInfo);
        }
        #endregion
    }
}