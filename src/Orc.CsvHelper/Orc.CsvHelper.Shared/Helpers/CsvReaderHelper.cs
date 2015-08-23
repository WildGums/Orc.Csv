// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Catel.IoC;
    using CsvHelper.Services;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    [ObsoleteEx(RemoveInVersion = "1.2", TreatAsErrorFromVersion = "1.1", ReplacementTypeOrMember = "ICsvReaderService")]
    public static class CsvReaderHelper
    {
        #region Methods
        public static IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T, TMap>(csvFilePath, initializer, csvConfiguration, throwOnError);
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T>(csvFilePath, initializer, mapType, csvConfiguration, throwOnError, culture);
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap map, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.ReadCsv<T>(csvFilePath, map, initializer, csvConfiguration, throwOnError, culture);
        }

        public static CsvReader CreateReader(string csvFilePath, Type classMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.CreateReader(csvFilePath, classMapType, csvConfiguration, culture);
        }

        public static CsvReader CreateReader(string csvFilePath, CsvClassMap classMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            return csvReaderService.CreateReader(csvFilePath, classMap, csvConfiguration, culture);
        }

        #endregion
    }
}