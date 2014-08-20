// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.IO;
    using CsvHelper;

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
    }
}