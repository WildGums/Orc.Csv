namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Catel.Logging;
    using Catel.Threading;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class CsvWriterService : ICsvWriterService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

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

        public virtual void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
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

                WriteRecords(records, typeof(TRecord), throwOnError, csvWriter);
            }
        }

        public virtual void WriteCsv<TRecord>(IEnumerable<TRecord> records, StreamWriter streamWriter, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
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

                WriteRecords(records, typeof(TRecord), throwOnError, csvWriter);
            }
        }

        public virtual void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap
        {
            WriteCsv(records, csvFilePath, typeof (TMap), csvConfiguration, throwOnError, cultureInfo);
        }
        
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

        public virtual void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            WriteCsv(records, csvFilePath, typeof(TRecord), csvMap, csvConfiguration, throwOnError, cultureInfo);
        }

        public virtual async Task WriteCsvAsync<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap
        {
            await WriteCsvAsync(records, csvFilePath, typeof(TMap), csvConfiguration, throwOnError, cultureInfo);
        }

        public virtual async Task WriteCsvAsync<TRecord>(IEnumerable<TRecord> records, StreamWriter streamWriter, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            await TaskHelper.Run(() => WriteCsv(records, streamWriter, csvMap, csvConfiguration, throwOnError, cultureInfo), false, CancellationToken.None);
        }

        public virtual async Task WriteCsvAsync(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            await TaskHelper.Run(() => WriteCsv(records, csvFilePath, recordType, csvMap, csvConfiguration, throwOnError, cultureInfo), false, CancellationToken.None);
        }

        public virtual async Task WriteCsvAsync(IEnumerable records, StreamWriter streamWriter, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            await TaskHelper.Run(() => WriteCsv(records, streamWriter, recordType, csvMap, csvConfiguration, throwOnError, cultureInfo), false, CancellationToken.None);
        }

        public virtual async Task WriteCsvAsync<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            await WriteCsvAsync(records, csvFilePath, typeof(TRecord), csvMap, csvConfiguration, throwOnError, cultureInfo);
        }

        public virtual async Task WriteCsvAsync<TRecord>(IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            await TaskHelper.Run(() => WriteCsv(records, csvFilePath, csvMap, csvConfiguration, throwOnError, cultureInfo), false, CancellationToken.None);
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
    }
}