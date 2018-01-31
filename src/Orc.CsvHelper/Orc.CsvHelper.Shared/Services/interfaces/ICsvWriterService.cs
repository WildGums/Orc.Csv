// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvWriterService
    {
        #region Methods
        Task WriteCsvAsync(IEnumerable records, string csvFilePath, Type recordType, ClassMap csvMap, Configuration configuration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv(IEnumerable records, StreamWriter streamWriter, Type recordType, ClassMap csvMap, Configuration configuration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv(IEnumerable records, string csvFilePath, Type recordType, ClassMap csvMap, Configuration configuration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        Configuration CreateDefaultConfiguration(CultureInfo cultureInfo = null);

        CsvWriter CreateWriter(string csvFilePath, Configuration configuration = null);

        CsvWriter CreateWriter(StreamWriter streamWriter, Configuration configuration = null);

        void WriteRecord(CsvWriter writer, params object[] fields);
        #endregion
    }
}