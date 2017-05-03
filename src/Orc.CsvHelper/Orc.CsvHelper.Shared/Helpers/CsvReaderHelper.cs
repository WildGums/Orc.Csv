// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderHelper.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Catel.IoC;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", ReplacementTypeOrMember = "ICsvReaderService")]
    public static class CsvReaderHelper
    {
        #region Methods
        public static IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true)
            where TMap : CsvClassMap
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T, TMap>(csvFilePath, initializer, csvConfiguration, throwOnError);
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T>(csvFilePath, initializer, mapType, csvConfiguration, throwOnError, culture);
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap csvMap, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T>(csvFilePath, csvMap, initializer, csvConfiguration, throwOnError, culture);
        }

        public static CsvReader CreateReader(string csvFilePath, Type csvMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.CreateReader(csvFilePath, csvMapType, csvConfiguration, culture);
        }

        public static CsvReader CreateReader(string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.CreateReader(csvFilePath, csvMap, csvConfiguration, culture);
        }

        #endregion
    }
}