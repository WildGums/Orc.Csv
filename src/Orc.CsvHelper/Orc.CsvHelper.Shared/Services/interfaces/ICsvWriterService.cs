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
        Task WriteCsvAsync(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv(IEnumerable records, StreamWriter streamWriter, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        CsvConfiguration CreateDefaultCsvConfiguration(CultureInfo cultureInfo = null);

        CsvWriter CreateWriter(string csvFilePath, CsvConfiguration csvConfiguration = null);

        CsvWriter CreateWriter(StreamWriter streamWriter, CsvConfiguration csvConfiguration = null);

        void WriteRecord(CsvWriter writer, params object[] fields);

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterServiceExtensions")]
        void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterServiceExtensions")]
        void WriteCsv<TRecord>(IEnumerable<TRecord> records, StreamWriter streamWriter, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterServiceExtensions")]
        void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterServiceExtensions")]
        void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap;
        #endregion
    }
}