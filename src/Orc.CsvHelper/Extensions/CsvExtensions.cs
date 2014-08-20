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
    /// </summary>
    public static class CsvExtensions
    {
        #region Constants
        public static readonly DateTime ExcelNullDate = new DateTime(1900, 1, 1, 0, 0, 0);
        #endregion

        #region Methods
        public static void ToCsv<T>(this IEnumerable<T> records, string path, Type csvMap = null)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                using (var csv = new CsvWriter(streamWriter))
                {
                    csv.Configuration.CultureInfo = new CultureInfo("en-AU");
                    csv.Configuration.WillThrowOnMissingField = false;
                    csv.Configuration.HasHeaderRecord = true;

                    if (csvMap != null)
                    {
                        csv.Configuration.RegisterClassMap(csvMap);
                    }

                    csv.WriteHeader<T>();
                    csv.WriteRecords(records);
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
        public static List<T> FromCsvFile<T>(string filePath, Type classMap = null)
        {
            using (var reader = CreateCsvReader(filePath, classMap))
            {
                var records = reader.GetRecords<T>().ToList();
                return records;
            }
        }

        public static CsvReader CreateCsvReader(string filePath, Type classMapType = null)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new StreamReader(fs, Encoding.Default);

            var csvReader = new CsvReader(stream);
            csvReader.Configuration.CultureInfo = new CultureInfo("en-AU");
            csvReader.Configuration.WillThrowOnMissingField = false;
            csvReader.Configuration.SkipEmptyRecords = true;
            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.TrimFields = true;

            if (classMapType != null)
            {
                csvReader.Configuration.RegisterClassMap(classMapType);
            }

            return csvReader;
        }

        public static IEnumerable<T> GetRecords<T>(string filePaath, Type classMapType = null)
        {
            using (var csvReader = CreateCsvReader(filePaath, classMapType))
            {
                try
                {
                    var records = csvReader.GetRecords<T>().ToArray();
                    return records;
                }
                catch (Exception ex)
                {
                    // In debug mode we can read the message and know which line and column has a problem
                    // Probably need to deal with that more elegantly.
                    var message = ex.Data["CsvHelper"];

                    if (string.Equals(ex.Message, "No header record was found.", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return new T[0];
                    }

                    throw;
                }
            }
        }

        public static IEnumerable<T> GetRecords<T, TMap>(string filePaath)
        {
            return GetRecords<T>(filePaath, typeof(TMap));
        }
        #endregion
    }
}