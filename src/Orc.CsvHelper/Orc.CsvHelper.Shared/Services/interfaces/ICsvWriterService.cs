namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvWriterService
    {
        CsvWriter CreateWriter(string csvFilePath, CsvConfiguration csvConfiguration = null);

        void WriteRecord(CsvWriter writer, params object[] fields);

        void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap;

        void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);

        void WriteCsv(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null);
    }
}