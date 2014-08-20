// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderHelper.cs" company="Simulation Modelling Services">
//   Copyright (c) 2008 - 2014 Simulation Modelling Services. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;

    public static class CsvReaderHelper
    {
        #region Methods
        public static IEnumerable<T> ReadCsv<T, TMap>(string csvFileName)
            where TMap : CsvClassMap
        {
            using (var streamReader = new StreamReader(csvFileName))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.RegisterClassMap<TMap>();

                    return csvReader.GetRecords<T>().ToList();
                }
            }
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