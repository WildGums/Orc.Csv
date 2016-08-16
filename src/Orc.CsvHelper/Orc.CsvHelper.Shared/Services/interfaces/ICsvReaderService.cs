// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    public interface ICsvReaderService
    {
        #region Methods
        IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap;

        IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null);

        IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap csvMap, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null);

        CsvReader CreateReader(string csvFilePath, Type csvMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null);

        CsvReader CreateReader(string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null);
        #endregion
    }
}