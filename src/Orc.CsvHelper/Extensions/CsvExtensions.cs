// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensions.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Catel.Logging;
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
        public static void ToCsv<TRecord, TMap>(this IEnumerable<TRecord> records, string csvFilePath, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
        {
            ToCsv(records, csvFilePath, typeof(TMap), csvConfiguration);
        }

        public static void ToCsv<TRecord>(this IEnumerable<TRecord> records, string csvFilePath, Type csvMap = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
        {
            if (csvConfiguration == null)
            {
                csvConfiguration = new CsvConfiguration();
                csvConfiguration.CultureInfo = CsvEnvironment.DefaultCultureInfo;
                csvConfiguration.WillThrowOnMissingField = false;
                csvConfiguration.HasHeaderRecord = true;
                //csvConfiguration.IgnorePrivateAccessor = true;
            }

            using (var csvWriter = CsvWriterHelper.CreateWriter(csvFilePath, csvConfiguration))
            {
                if (csvMap != null)
                {
                    csvWriter.Configuration.RegisterClassMap(csvMap);
                }

                try
                {
                    csvWriter.WriteHeader<TRecord>();
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
        #endregion
    }
}