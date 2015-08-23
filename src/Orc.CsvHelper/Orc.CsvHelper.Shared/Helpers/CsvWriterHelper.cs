// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    public static class CsvWriterHelper
    {
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

        public static void WriteRecord(this CsvWriter writer, params object[] fields)
        {
            foreach (var field in fields)
            {
                writer.WriteField(field);
            }

            writer.NextRecord();
        }

        public static void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
        {
            Type csvMapType = null;
            records.ToCsv(csvFilePath, csvMapType, csvConfiguration, throwOnError);
        }

        public static void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap
        {
            records.ToCsv(csvFilePath, typeof(TMap), csvConfiguration, throwOnError);
        }
    }
}