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
        public virtual IEnumerable<T> ReadCsv<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
        {
            using (var csvReader = CreateReader(csvFilePath, csvMap, configuration, cultureInfo))
            {
                return ReadData(csvFilePath, initializer, throwOnError, csvReader);
            }
        }

        public async Task<IList<T>> ReadCsvAsync<T>(string csvFilePath, ClassMap csvMap, Action<T> initializer = null, Configuration configuration = null, bool throwOnError = true, CultureInfo cultureInfo = null)
        {
            if (!_fileService.Exists(csvFilePath))
            {
                throw Log.ErrorAndCreateException<FileNotFoundException>("File '{0}' doesn't exist", csvFilePath);
            }

            var buffer = await _fileService.ReadAllBytesAsync(csvFilePath);
            configuration = CreateConfiguration(configuration, cultureInfo);

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

        public CsvReader CreateReader(string csvFilePath, ClassMap csvMap, Configuration configuration = null, CultureInfo cultureInfo = null)
        {
            var csvReader = CreateReaderCore(csvFilePath, configuration, cultureInfo);

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

        private CsvReader CreateReaderCore(string csvFilePath, Configuration configuration = null, CultureInfo cultureInfo = null)
        {
            configuration = CreateConfiguration(configuration, cultureInfo);
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

        protected virtual Configuration CreateDefaultConfiguration(CultureInfo cultureInfo)
        {
            var configuration = new Configuration
            {
                CultureInfo = cultureInfo ?? CsvEnvironment.DefaultCultureInfo,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
            };

            return configuration;
        }

        private Configuration CreateConfiguration(Configuration configuration, CultureInfo cultureInfo)
        {
            if (configuration != null && cultureInfo != null)
            {
                configuration.CultureInfo = cultureInfo;
            }

            return configuration ?? CreateDefaultConfiguration(cultureInfo);
        }
        #endregion
    }
}