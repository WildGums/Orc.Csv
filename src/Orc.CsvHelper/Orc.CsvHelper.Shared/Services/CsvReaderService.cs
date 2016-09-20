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
    using Catel;
    using Catel.Logging;
    using Csv;
    using FileSystem;
    using global::CsvHelper;
    using global::CsvHelper.Configuration;

    public class CsvReaderService : ICsvReaderService
    {
        private readonly IFileService _fileService;

        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        public CsvReaderService(IFileService fileService)
        {
            Argument.IsNotNull(() => fileService);

            _fileService = fileService;
        }

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

        public CsvReader CreateReader(string csvFilePath, Type csvMapType = null, CsvConfiguration csvConfiguration = null, CultureInfo culture = null)
        {
            if (csvConfiguration != null && culture != null)
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
            if (csvConfiguration != null && culture != null)
            {
                csvConfiguration.CultureInfo = culture;
            }

            var csvReader = CreateCsvReader(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(culture));

            csvReader.Configuration.RegisterClassMap(csvMap);

            return csvReader;
        }

        protected virtual CsvReader CreateCsvReader(string csvFilePath, CsvConfiguration csvConfiguration)
        {
            if (!_fileService.Exists(csvFilePath))
            {
                throw Log.ErrorAndCreateException<FileNotFoundException>("File '{0}' doesn't exist", csvFilePath);
            }

            var fileStream = _fileService.Open(csvFilePath, FileMode.Open, FileAccess.Read);
            var stream = new StreamReader(fileStream, Encoding.Default);

            var csvReader = new CsvReader(stream, csvConfiguration);
            return csvReader;
        }

        protected virtual CsvConfiguration CreateDefaultCsvConfiguration(CultureInfo culture)
        {
            var configuration = new CsvConfiguration();
            configuration.CultureInfo = culture ?? CsvEnvironment.DefaultCultureInfo;
            configuration.WillThrowOnMissingField = false;
            configuration.SkipEmptyRecords = true;
            configuration.HasHeaderRecord = true;
            configuration.TrimFields = true;
            configuration.TrimHeaders = true;
            return configuration;
        }
        #endregion
    }
}