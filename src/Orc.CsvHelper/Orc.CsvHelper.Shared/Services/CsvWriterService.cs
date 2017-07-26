// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FileSystem;

    public class CsvWriterService : ICsvWriterService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public CsvWriterService(IFileService fileService)
        {
            Argument.IsNotNull(() => fileService);

            _fileService = fileService;
        }
        #endregion

        #region ICsvWriterService Members
        public virtual void WriteCsv(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration != null && cultureInfo != null)
            {
                csvConfiguration.CultureInfo = cultureInfo;
            }

            using (var csvWriter = CreateWriter(csvFilePath, csvConfiguration ?? CreateDefaultCsvConfiguration(cultureInfo)))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, recordType, throwOnError, csvWriter);
            }
        }

        public virtual void WriteCsv(IEnumerable records, StreamWriter streamWriter, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration != null && cultureInfo != null)
            {
                csvConfiguration.CultureInfo = cultureInfo;
            }

            using (var csvWriter = CreateWriter(streamWriter, csvConfiguration ?? CreateDefaultCsvConfiguration(cultureInfo)))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, recordType, throwOnError, csvWriter);
            }
        }

        public virtual async Task WriteCsvAsync(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            byte[] buffer = null;
            using (var memoryStream = new MemoryStream())
            {
                WriteCsv(records, new StreamWriter(memoryStream), recordType, csvMap, csvConfiguration, throwOnError, cultureInfo);
                buffer = memoryStream.ToArray();
            }

            using (var fileStream = _fileService.Create(csvFilePath))
            {
                await fileStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public virtual CsvConfiguration CreateDefaultCsvConfiguration(CultureInfo cultureInfo = null)
        {
            var csvConfiguration = new CsvConfiguration
            {
                CultureInfo = cultureInfo ?? CsvEnvironment.DefaultCultureInfo,
                WillThrowOnMissingField = false,
                HasHeaderRecord = true
            };

            return csvConfiguration;
        }

        public virtual CsvWriter CreateWriter(string csvFilePath, CsvConfiguration csvConfiguration = null)
        {
            var streamWriter = new StreamWriter(csvFilePath, false);
            return CreateWriter(streamWriter, csvConfiguration);
        }

        public virtual CsvWriter CreateWriter(StreamWriter streamWriter, CsvConfiguration csvConfiguration = null)
        {
            return new CsvWriter(streamWriter, csvConfiguration ?? CreateDefaultCsvConfiguration());
        }

        public virtual void WriteRecord(CsvWriter writer, params object[] fields)
        {
            foreach (var field in fields)
            {
                writer.WriteField(field);
            }

            writer.NextRecord();
        }
        #endregion

        #region Methods
        protected virtual void WriteRecords(IEnumerable records, Type recordType, bool throwOnError, CsvWriter csvWriter)
        {
            try
            {
                csvWriter.WriteHeader(recordType);
                csvWriter.WriteRecords(records);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot write row to csv. Error Details: {0}", errorMessage);

                if (throwOnError)
                {
                    throw;
                }
            }
        }
        #endregion
    }
}