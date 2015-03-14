// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensions.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions to read csv files that are already open by another application.
    /// TODO: Accept a configuration
    /// TODO: Ability to change culture
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
        #region Methods
        public static void ToCsv<TRecord, TMap>(this IEnumerable<TRecord> records, string path)
        {
            ToCsv(records, path, typeof(TMap));
        }

        public static void ToCsv<TRecord>(this IEnumerable<TRecord> records, string path, Type csvMap = null)
        {
            using (var csvWriter = CsvWriterHelper.CreateWriter(path))
            {
                csvWriter.Configuration.CultureInfo = CsvEnvironment.DefaultCultureInfo;
                csvWriter.Configuration.WillThrowOnMissingField = false;
                csvWriter.Configuration.HasHeaderRecord = true;
                //csvWriter.Configuration.IgnorePrivateAccessor = true;

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
                    var message = ex.Data["CsvHelper"];
                    throw;
                }
            }
        }
        #endregion
    }
}