// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper;
    using CsvHelper.Configuration;

    public static class ICsvReaderServiceExtensions
    {
        #region Methods
        public static Task<IList<T>> ReadCsvAsync<T, TMap>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
            where TMap : CsvClassMap
        {
            Argument.IsNotNull(() => csvReaderService);

            return ReadCsvAsync<T>(csvReaderService, csvFilePath, typeof(TMap), initializer, csvConfiguration, throwOnError, culture);
        }

        public static Task<IList<T>> ReadCsvAsync<T>(this ICsvReaderService csvReaderService, string csvFilePath, Type csvMapType = null, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMapType);
            return csvReaderService.ReadCsvAsync(csvFilePath, csvMapInstance, initializer, csvConfiguration, throwOnError, culture);
        }

        public static IEnumerable<T> ReadCsv<T, TMap>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
            where TMap : CsvClassMap
        {
            Argument.IsNotNull(() => csvReaderService);

            return csvReaderService.ReadCsv<T>(csvFilePath, initializer, typeof(TMap), csvConfiguration, throwOnError, culture);
        }

        public static IEnumerable<T> ReadCsv<T>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, Type csvMapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMapType);
            return csvReaderService.ReadCsv(csvFilePath, csvMapInstance, initializer, csvConfiguration, throwOnError, culture);
        }

        public static CsvReader CreateReader(this ICsvReaderService csvReaderService, string csvFilePath, Type csvMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateCsvClassMap(csvMapType);
            return csvReaderService.CreateReader(csvFilePath, csvMapInstance, csvConfiguration, culture);
        }
        #endregion
    }
}