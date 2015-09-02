namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class CsvWriterService : ICsvWriterService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        public virtual CsvWriter CreateWriter(string csvFilePath, CsvConfiguration csvConfiguration = null)
        {
            // No disposes are required, the user should dispose the csv class
            var streamWriter = new StreamWriter(csvFilePath, false);

            return new CsvWriter(streamWriter, csvConfiguration ?? new CsvConfiguration());
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
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, typeof(TRecord), csvFilePath, throwOnError, csvWriter);
            }
        }

        public virtual void WriteCsv<TRecord, TMap>(IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
            where TMap : CsvClassMap
        {
            WriteCsv<TRecord>(records, csvFilePath, typeof (TMap), csvConfiguration, throwOnError, cultureInfo);
        }

        public virtual void WriteCsv<TRecord>(IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, typeof(TRecord), csvFilePath, throwOnError, csvWriter);
            }
        }

        public virtual void WriteCsv(IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, recordType, csvFilePath, throwOnError, csvWriter);
            }
        }

        protected virtual CsvConfiguration CreateCsvConfiguration(CultureInfo cultureInfo = null)
        {
            var csvConfiguration = new CsvConfiguration
            {
                CultureInfo = cultureInfo ?? CsvEnvironment.DefaultCultureInfo,
                WillThrowOnMissingField = false,
                HasHeaderRecord = true
                //csvConfiguration.IgnorePrivateAccessor = true;
            };

            return csvConfiguration;
        }

        protected virtual void WriteRecords(IEnumerable records, Type recordType, string csvFilePath, bool throwOnError, CsvWriter csvWriter)
        {
            try
            {
                csvWriter.WriteHeader(recordType);
                csvWriter.WriteRecords(records);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot read row in '{0}'. Error Details: {1}", Path.GetFileName(csvFilePath), errorMessage);

                if (throwOnError)
                {
                    throw;
                }
            }
        }


    }
}