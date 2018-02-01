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
        public static Task<IList<T>> ReadCsvAsync<T, TMap>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
            where TMap : ClassMap
        {
            Argument.IsNotNull(() => csvReaderService);

            return ReadCsvAsync<T>(csvReaderService, csvFilePath, typeof(TMap), initializer, configuration, throwOnError, cultureInfo);
        }

        public static Task<IList<T>> ReadCsvAsync<T>(this ICsvReaderService csvReaderService, string csvFilePath, Type csvMapType = null, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateClassMap(csvMapType);
            return csvReaderService.ReadCsvAsync(csvFilePath, csvMapInstance, initializer, configuration, throwOnError, cultureInfo);
        }

        public static IEnumerable<T> ReadCsv<T, TMap>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
            where TMap : ClassMap
        {
            Argument.IsNotNull(() => csvReaderService);

            return csvReaderService.ReadCsv<T>(csvFilePath, initializer, typeof(TMap), configuration, throwOnError, cultureInfo);
        }

        public static IEnumerable<T> ReadCsv<T>(this ICsvReaderService csvReaderService, string csvFilePath, Action<T> initializer = null, Type csvMapType = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateClassMap(csvMapType);
            return csvReaderService.ReadCsv(csvFilePath, csvMapInstance, initializer, configuration, throwOnError, cultureInfo);
        }

        public static CsvReader CreateReader(this ICsvReaderService csvReaderService, string csvFilePath, Type csvMapType = null, Configuration configuration = null, CultureInfo cultureInfo = null)
        {
            Argument.IsNotNull(() => csvReaderService);

            var typeFactory = csvReaderService.GetTypeFactory();
            var csvMapInstance = typeFactory.TryToCreateClassMap(csvMapType);
            return csvReaderService.CreateReader(csvFilePath, csvMapInstance, configuration, cultureInfo);
        }
        #endregion
    }
}