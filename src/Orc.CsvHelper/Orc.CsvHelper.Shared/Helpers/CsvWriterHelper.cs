// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterHelper.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", ReplacementTypeOrMember = "ICsvWriterService")]
    public static class CsvWriterHelper
    {
        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterService")]
        public static CsvWriter CreateWriter(string csvFilePath, CsvConfiguration csvConfiguration = null)
        {
            // No disposes are required, the user should dispose the csv class
            var streamWriter = new StreamWriter(csvFilePath, false);

            if (csvConfiguration == null)
            {
                csvConfiguration = new CsvConfiguration();
            }

            return new CsvWriter(streamWriter, csvConfiguration);
        }

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterService")]
        public static void WriteRecord(this CsvWriter writer, params object[] fields)
        {
            foreach (var field in fields)
            {
                writer.WriteField(field);
            }

            writer.NextRecord();
        }

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterService")]
        public static void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
        {
            Type csvMapType = null;
            records.ToCsv(csvFilePath, csvMapType, csvConfiguration, throwOnError);
        }

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", Message = "use ICsvWriterService")]
        public static void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap
        {
            records.ToCsv(csvFilePath, typeof(TMap), csvConfiguration, throwOnError);
        }
    }
}