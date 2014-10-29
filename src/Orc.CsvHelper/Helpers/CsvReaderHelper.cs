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
            if (!File.Exists(csvFileName))
            {
                throw new FileNotFoundException(string.Format("{0} doesn't exist", csvFileName));
            }

            var items = new List<T>();

            using (var streamReader = new StreamReader(csvFileName))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.RegisterClassMap<TMap>();

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
                        var errorMessage = ex.Data["CsvHelper"].ToString();

                        Log.Warning("Cannot read row in '{0}'. Error Details: {1}", Path.GetFileName(csvFileName), errorMessage);
                    }
                }
            }

            return items;
        }

        public static CsvReader CreateReader(string csvFileName)
        {
            // No disposes are required, the user should dispose the csv class
            var streamReader = new StreamReader(csvFileName);

            return new CsvReader(streamReader);
        }
        #endregion
    }
}