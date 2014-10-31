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
        public static IEnumerable<T> ReadCsv<T, TMap>(string csvFileName, Action<T> initializer = null)
            where TMap : CsvClassMap
        {
            return ReadCsv<T>(csvFileName, initializer, typeof(TMap));
        }

        public static IEnumerable<T> ReadCsv<T>(string csvFileName, Action<T> initializer = null, Type mapType = null)
        {
            var items = new List<T>();

            using (var csvReader = CreateReader(csvFileName, mapType))
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
                    // In debug mode we can read the message and know which line and column has a problem
                    // Probably need to deal with that more elegantly.
                    var message = ex.Data["CsvHelper"];

                    if (string.Equals(ex.Message, "No header record was found.", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return new T[0];
                    }

                    var errorMessage = ex.Data["CsvHelper"].ToString();

                    Log.Warning("Cannot read row in '{0}'. Error Details: {1}", Path.GetFileName(csvFileName), errorMessage);
                }
            }

            return items;
        }



        public static CsvReader CreateReader(string csvFileName, Type classMapType = null)
        {
            if (!File.Exists(csvFileName))
            {
                throw new FileNotFoundException(string.Format("{0} doesn't exist", csvFileName));
            }

            // No disposes are required, the user should dispose the csv class
            var fs = new FileStream(csvFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new StreamReader(fs, Encoding.Default);

            var csvReader = new CsvReader(stream);
            csvReader.Configuration.CultureInfo = CsvEnvironment.DefaultCultureInfo;
            csvReader.Configuration.WillThrowOnMissingField = false;
            csvReader.Configuration.SkipEmptyRecords = true;
            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.TrimFields = true;

            if (classMapType != null)
            {
                csvReader.Configuration.RegisterClassMap(classMapType);
            }

            return csvReader;
        }
        #endregion
    }
}