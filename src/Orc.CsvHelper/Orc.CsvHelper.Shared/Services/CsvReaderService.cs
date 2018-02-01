// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvReaderService.cs" company="WildGums">
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
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class CsvReaderService : CsvServiceBase, ICsvReaderService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region ICsvReaderService Members
        public virtual IEnumerable ReadRecords(StreamReader streamReader, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => streamReader);
            Argument.IsNotNull(() => csvContext);

            using (var csvReader = CreateReader(streamReader, csvContext))
            {
                var data = ReadData(csvReader, csvContext);
                return data;
            }
        }

        public async Task<IEnumerable> ReadRecordsAsync(StreamReader streamReader, ICsvContext csvContext)
        {
            using (var csvReader = CreateReader(streamReader, csvContext))
            {
                var data = await ReadDataAsync(csvReader, csvContext);
                return data;
            }
        }

        public CsvReader CreateReader(StreamReader streamReader, ICsvContext csvContext)
        {
            var configuration = EnsureCorrectConfiguration(csvContext.Configuration, csvContext.CultureInfo);

            var csvReader = new CsvReader(streamReader, configuration);
            if (csvContext.ClassMap != null)
            {
                csvReader.Configuration.RegisterClassMap(csvContext.ClassMap);
            }

            return csvReader;
        }
        #endregion

        #region Methods
        protected virtual IEnumerable ReadData(CsvReader csvReader, ICsvContext csvContext)
        {
            var recordType = csvContext.RecordType;
            var initializer = csvContext.Initializer;
            var items = new List<object>();

            try
            {
                while (csvReader.Read())
                {
                    var record = csvReader.GetRecord(recordType);
                    if (initializer != null)
                    {
                        initializer(record);
                    }

                    items.Add(record);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.EqualsIgnoreCase("No header record was found."))
                {
                    return new object[0];
                }

                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot read row from stream. Error Details: {1}", errorMessage);

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }

            return items;
        }

        protected virtual async Task<IEnumerable> ReadDataAsync(CsvReader csvReader, ICsvContext csvContext)
        {
            var recordType = csvContext.RecordType;
            var initializer = csvContext.Initializer;
            var items = new List<object>();

            try
            {
                while (await csvReader.ReadAsync())
                {
                    var record = csvReader.GetRecord(recordType);
                    if (initializer != null)
                    {
                        initializer(record);
                    }

                    items.Add(record);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.EqualsIgnoreCase("No header record was found."))
                {
                    return new object[0];
                }

                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot read row from stream. Error Details: {1}", errorMessage);

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }

            return items;
        }
        #endregion
    }
}