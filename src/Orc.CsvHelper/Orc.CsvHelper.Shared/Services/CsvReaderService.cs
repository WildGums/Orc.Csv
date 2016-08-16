// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", ReplacementTypeOrMember = "ReadCsv with CsvClassMap signature")]
        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            using (var csvReader = CreateReader(csvFilePath, mapType, csvConfiguration, culture))
            {
                return ReadData(csvFilePath, initializer, throwOnError, csvReader);
            }
        }

        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, CsvClassMap csvMap, Action<T> initializer = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo culture = null)
        {
            using (var csvReader = CreateReader(csvFilePath, csvMap, csvConfiguration, culture))
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

        [ObsoleteEx(RemoveInVersion = "2.0", TreatAsErrorFromVersion = "1.1", ReplacementTypeOrMember = "CreateReader with CsvClassMap signature")]
        public CsvReader CreateReader(string csvFilePath, Type csvMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            if (csvConfiguration != null)
            {
                csvConfiguration.CultureInfo = culture;
            }

            var csvReader = CreateCsvReader(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(culture));

            if (csvMapType != null)
            {
                csvReader.Configuration.RegisterClassMap(csvMapType);
            }

            return csvReader;
        }

        public CsvReader CreateReader(string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            if (csvConfiguration != null)
            {
                csvConfiguration.CultureInfo = culture;
            }

            var csvReader = CreateCsvReader(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(culture));

            csvReader.Configuration.RegisterClassMap(csvMap);

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