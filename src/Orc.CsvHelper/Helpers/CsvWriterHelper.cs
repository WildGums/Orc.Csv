// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections.Generic;
    using System.IO;
    using CsvHelper;
    using CsvHelper.Configuration;

    public static class CsvWriterHelper
    {
        public static CsvWriter CreateWriter(string csvFileName)
        {
            // No disposes are required, the user should dispose the csv class
            var streamWriter = new StreamWriter(csvFileName, false);

            return new CsvWriter(streamWriter);
        }

        public static void WriteRecord(this CsvWriter writer, params object[] fields)
        {
            foreach (var field in fields)
            {
                writer.WriteField(field);
            }

            writer.NextRecord();
        }

        public static void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFileName)
        {
            records.ToCsv(csvFileName);
        }

        public static void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFileName)
            where TMap : CsvClassMap
        {
            records.ToCsv(csvFileName, typeof(TMap));
        }
    }
}