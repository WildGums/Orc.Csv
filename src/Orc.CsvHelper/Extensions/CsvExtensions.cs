// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensions.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CsvHelper;

    /// <summary>
    /// Extensions to read csv files that are already open by another application.
    /// TODO: Accept a configuration
    /// TODO: Ability to change culture
    /// Standard configuration for csv Reader:
    /// 
    /// csvReader.Configuration.CultureInfo = new CultureInfo("en-AU");
    /// Configuration.WillThrowOnMissingField = false;
    /// Configuration.SkipEmptyRecords = true;
    /// Configuration.HasHeaderRecord = true;
    /// Configuration.TrimFields = true;
    /// </summary>
    public static class CsvExtensions
    {
        #region Methods
        public static void ToCsv<T>(this IEnumerable<T> records, string path, Type csvMap = null)
        {
            using (var csvWriter = CsvWriterHelper.CreateWriter(path))
            {
                csvWriter.Configuration.CultureInfo = CsvEnvironment.DefaultCultureInfo;
                csvWriter.Configuration.WillThrowOnMissingField = false;
                csvWriter.Configuration.HasHeaderRecord = true;
                //csvWriter.Configuration.IgnorePrivateAccessor = true;

                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                try
                {
                    csvWriter.WriteHeader<T>();
                    csvWriter.WriteRecords(records);
                }
                catch (Exception ex)
                {
                    var message = ex.Data["CsvHelper"];
                    throw;
                }
            }
        }

        /// <summary>
        /// Read a csv file and return the row data as objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The full file path of the csv file</param>
        /// <param name="classMap">The class map to use</param>
        /// <returns></returns>
        [ObsoleteEx(Replacement = "CsvReaderHelper.ReadCsv", TreatAsErrorFromVersion = "0.1", RemoveInVersion = "1.0")]
        public static List<T> FromCsvFile<T>(string filePath, Type classMap = null)
        {
            throw new NotImplementedException();
        }

        [ObsoleteEx(Replacement = "CsvReaderHelper.CreateReader", TreatAsErrorFromVersion = "0.1", RemoveInVersion = "1.0")]
        public static CsvReader CreateCsvReader(string filePath, Type classMapType = null)
        {
            throw new NotImplementedException();
        }

        [ObsoleteEx(Replacement = "CsvReaderHelper.ReadCsv", TreatAsErrorFromVersion = "0.1", RemoveInVersion = "1.0")]
        public static IEnumerable<T> GetRecords<T>(string filePath, Type classMapType = null)
        {
            throw new NotImplementedException();
        }

        [ObsoleteEx(Replacement = "CsvReaderHelper.ReadCsv", TreatAsErrorFromVersion = "0.1", RemoveInVersion = "1.0")]
        public static IEnumerable<T> GetRecords<T, TMap>(string filePath)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}