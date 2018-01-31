// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
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
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FileSystem;

    public class CsvReaderService : ICsvReaderService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public CsvReaderService(IFileService fileService)
        {
            Argument.IsNotNull(() => fileService);

            _fileService = fileService;
        }
        #endregion

        #region ICsvReaderService Members
        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            using (var csvReader = CreateReader(csvFilePath, csvMap, configuration, culture))
            {
                return ReadData(csvFilePath, initializer, throwOnError, csvReader);
            }
        }

        public async Task<IList<T>> ReadCsvAsync<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo culture = null)
        {
            if (!_fileService.Exists(csvFilePath))
            {
                throw Log.ErrorAndCreateException<FileNotFoundException>("File '{0}' doesn't exist", csvFilePath);
            }

            var buffer = await _fileService.ReadAllBytesAsync(csvFilePath);
            configuration = CreateConfiguration(configuration, culture);

            using (var memoryStream = new MemoryStream(buffer))
            {
                var stream = new StreamReader(memoryStream, Encoding.Default);
                using (var csvReader = new CsvReader(stream, configuration))
                {
                    if (csvMap != null)
                    {
                        csvReader.Configuration.RegisterClassMap(csvMap);
                    }

                    return ReadData(csvFilePath, initializer, throwOnError, csvReader).ToList();
                }
            }
        }

        public CsvReader CreateReader(string csvFilePath, ClassMap csvMap, Configuration configuration = null, CultureInfo culture = null)
        {
            var csvReader = CreateReaderCore(csvFilePath, configuration, culture);

            if (csvMap != null)
            {
                csvReader.Configuration.RegisterClassMap(csvMap);
            }

            return csvReader;
        }
        #endregion

        #region Methods
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
                if (ex.Message.EqualsIgnoreCase("No header record was found."))
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

        private CsvReader CreateReaderCore(string csvFilePath, Configuration configuration = null, CultureInfo culture = null)
        {
            configuration = CreateConfiguration(configuration, culture);
            var csvReader = CreateCsvReader(csvFilePath, configuration);
            return csvReader;
        }

        protected virtual CsvReader CreateCsvReader(string csvFilePath, Configuration configuration)
        {
            if (!_fileService.Exists(csvFilePath))
            {
                throw Log.ErrorAndCreateException<FileNotFoundException>("File '{0}' doesn't exist", csvFilePath);
            }

            var fileStream = _fileService.Open(csvFilePath, FileMode.Open, FileAccess.Read);
            var stream = new StreamReader(fileStream, Encoding.Default);

            var csvReader = new CsvReader(stream, configuration);
            return csvReader;
        }

        protected virtual Configuration CreateDefaultConfiguration(CultureInfo culture)
        {
            var configuration = new Configuration
            {
                CultureInfo = culture ?? CsvEnvironment.DefaultCultureInfo,
                WillThrowOnMissingField = false,
                SkipEmptyRecords = true,
                HasHeaderRecord = true,
                TrimFields = true,
                TrimHeaders = true
            };
            return configuration;
        }

        private Configuration CreateConfiguration(Configuration configuration, CultureInfo culture)
        {
            if (configuration != null && culture != null)
            {
                configuration.CultureInfo = culture;
            }

            return configuration ?? CreateDefaultConfiguration(culture);
        }
        #endregion
    }
}