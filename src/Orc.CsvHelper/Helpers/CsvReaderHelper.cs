// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;

    public static class CsvReaderHelper
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Methods
        public static IEnumerable<T> ReadCsv<T, TMap>(string csvFilePath, Action<T> initializer = null)
            where TMap : CsvClassMap
        {
            return ReadCsv<T>(csvFilePath, initializer, typeof(TMap));
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFilePath, Action<T> initializer = null, Type mapType = null, CsvConfiguration csvConfiguration = null, bool throwOnError = false)
        {
            var items = new List<T>();

            using (var csvReader = CreateReader(csvFilePath, mapType, csvConfiguration))
            {
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
            }

            return items;
        }



        public static CsvReader CreateReader(string csvFilePath, Type classMapType = null, CsvConfiguration csvConfiguration = null)
        {
            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException(string.Format("{0} doesn't exist", csvFilePath));
            }

            // No disposes are required, the user should dispose the csv class
            var fs = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new StreamReader(fs, Encoding.Default);

            if (csvConfiguration == null)
            {
                csvConfiguration = new CsvConfiguration();
                csvConfiguration.CultureInfo = CsvEnvironment.DefaultCultureInfo;
                csvConfiguration.WillThrowOnMissingField = false;
                csvConfiguration.SkipEmptyRecords = true;
                csvConfiguration.HasHeaderRecord = true;
                csvConfiguration.TrimFields = true;
                csvConfiguration.TrimHeaders = true;
            }

            var csvReader = new CsvReader(stream, csvConfiguration);

            if (classMapType != null)
            {
                csvReader.Configuration.RegisterClassMap(classMapType);
            }

            return csvReader;
        }
        #endregion
    }
}