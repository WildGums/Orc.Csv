// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensions.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


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

    /// <summary>
    /// Extensions to read csv files that are already open by another application.
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
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Methods
        public static void ToCsv<TRecord, TMap>(this IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            ToCsv(records, csvFilePath, typeof(TMap), csvConfiguration, throwOnError, cultureInfo);
        }

        public static void ToCsv<TRecord>(this IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CsvWriterHelper.CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, typeof(TRecord), csvFilePath, throwOnError, csvWriter);
            }
        }

        private static CsvConfiguration CreateCsvConfiguration(CultureInfo cultureInfo = null)
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

        public static void ToCsv<TRecord>(this IEnumerable<TRecord> records, string csvFilePath, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CsvWriterHelper.CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, typeof(TRecord), csvFilePath, throwOnError, csvWriter);
            }
        }

        public static void ToCsv(this IEnumerable records, string csvFilePath, Type recordType, CsvClassMap csvMap, CsvConfiguration csvConfiguration = null, bool throwOnError = false, CultureInfo cultureInfo = null)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = CreateCsvConfiguration(cultureInfo);
            }

            using (var csvWriter = CsvWriterHelper.CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                WriteRecords(records, recordType, csvFilePath, throwOnError, csvWriter);
            }
        }

        private static void WriteRecords(IEnumerable records, Type recordType, string csvFilePath, bool throwOnError, CsvWriter csvWriter)
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
        #endregion
    }
}