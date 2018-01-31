// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvReaderService
    {
        #region Methods
        Task<IList<T>> ReadCsvAsync<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo culture = null);

        IEnumerable<T> ReadCsv<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo culture = null);

        CsvReader CreateReader(string csvFilePath, ClassMap csvMap, Configuration configuration = null, CultureInfo culture = null);
        #endregion
    }
}