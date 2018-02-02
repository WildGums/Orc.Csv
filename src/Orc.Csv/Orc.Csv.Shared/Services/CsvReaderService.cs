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
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using CsvHelper;

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
            var configuration = EnsureCorrectConfiguration(csvContext.Configuration, csvContext);
            var csvReader = new CsvReader(streamReader, configuration);
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
                    if (record == null)
                    {
                        Log.Debug($"Read record resulting in null at row '{csvReader.Context.Row}'");
                        continue;
                    }

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