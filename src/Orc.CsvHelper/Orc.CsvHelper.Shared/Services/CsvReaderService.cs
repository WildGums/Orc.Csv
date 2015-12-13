// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderService.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2015 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Catel.Logging;
    using Csv;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    public class CsvReaderService : ICsvReaderService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        public virtual IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
            where TMap : CsvClassMap
        {
            return ReadCsv<T>(csvFilePath, initializer, typeof (TMap), csvConfiguration, throwOnError);
        }

        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            using (var csvReader = CreateReader(csvFilePath, mapType, csvConfiguration, culture))
            {
                return ReadData(csvFilePath, initializer, throwOnError, csvReader);
            }
        }

        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap map, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            using (var csvReader = CreateReader(csvFilePath, map, csvConfiguration, culture))
            {
                return ReadData(csvFilePath, initializer, throwOnError, csvReader);
            }
        }

        protected virtual IEnumerable<T> ReadData<T>(string csvFilePath, Action<T> initializer, bool throwOnError, CsvReader csvReader)
        {
            var items = new List<T>();

            try
            {
                while (csvReader.Read())
                {
                    var record = csvReader.GetRecord<T>();
                    if (initializer != null)
                    {
                        initializer(record);
                    }

                    items.Add(record);
                }
            }
            catch (Exception ex)
            {
                if (string.Equals(ex.Message, "No header record was found.", StringComparison.InvariantCultureIgnoreCase))
                {
                    return new T[0];
                }

                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot read row in '{0}'. Error Details: {1}", Path.GetFileName(csvFilePath), errorMessage);

                if (throwOnError)
                {
                    throw;
                }
            }

            return items;
        }

        public CsvReader CreateReader(string csvFilePath, Type classMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var csvReader = CreateCsvReader(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(culture));

            if (classMapType != null)
            {
                csvReader.Configuration.RegisterClassMap(classMapType);
            }

            return csvReader;
        }

        public CsvReader CreateReader(string csvFilePath, CsvClassMap classMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            var csvReader = CreateCsvReader(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(culture));

            csvReader.Configuration.RegisterClassMap(classMap);

            return csvReader;
        }

        protected virtual CsvReader CreateCsvReader(string csvFilePath, CsvConfiguration csvConfiguration)
        {
            if (!File.Exists(csvFilePath))
            {
                throw Log.ErrorAndCreateException<FileNotFoundException>("File '{0}' doesn't exist", csvFilePath);
            }

            var fs = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new StreamReader(fs, Encoding.Default);

            var csvReader = new CsvReader(stream, csvConfiguration);
            return csvReader;
        }

        protected virtual CsvConfiguration CreateDefaultCsvConfiguration(CultureInfo culture)
        {
            var csvConfiguration1 = new CsvConfiguration();
            csvConfiguration1.CultureInfo = culture ?? CsvEnvironment.DefaultCultureInfo;
            csvConfiguration1.WillThrowOnMissingField = false;
            csvConfiguration1.SkipEmptyRecords = true;
            csvConfiguration1.HasHeaderRecord = true;
            csvConfiguration1.TrimFields = true;
            csvConfiguration1.TrimHeaders = true;
            return csvConfiguration1;
        }
        #endregion
    }
}