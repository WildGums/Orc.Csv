﻿// --------------------------------------------------------------------------------------------------------------------
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

            if (csvContext.ClassMap is not null)
            {
                csvReader.Context.RegisterClassMap(csvContext.ClassMap);
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
                var configuration = csvReader.Configuration;
                if (configuration.HasHeaderRecord && csvReader.Context.Reader.HeaderRecord is null)
                {
                    Log.DebugIfAttached("Reading header");

                    // Yes, we need a double read
                    csvReader.Read();
                    csvReader.ReadHeader();
                }

                Log.DebugIfAttached("Reading records");

                while (csvReader.Read())
                {
                    AddCurrentRecord(csvReader, items, recordType, initializer, csvContext);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.EqualsIgnoreCase("No header record was found."))
                {
                    return new object[0];
                }

                Log.Warning(ex, "Failed to read data");

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
                var configuration = csvReader.Configuration;
                if (configuration.HasHeaderRecord && csvReader.Context.Reader.HeaderRecord is null)
                {
                    Log.DebugIfAttached("Reading header");

                    // Yes, we need a double read
                    await csvReader.ReadAsync();
                    csvReader.ReadHeader();
                }

                Log.DebugIfAttached("Reading records");

                while (await csvReader.ReadAsync())
                {
                    AddCurrentRecord(csvReader, items, recordType, initializer, csvContext);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.EqualsIgnoreCase("No header record was found."))
                {
                    return new object[0];
                }

                Log.Warning(ex, "Failed to read data");

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }

            return items;
        }

        private void AddCurrentRecord(CsvReader csvReader, List<object> items, Type recordType, Action<object> initializer, ICsvContext csvContext)
        {
            var record = ReadRecord(csvReader, recordType, csvContext);
            if (record is null)
            {
                Log.DebugIfAttached($"Read record results in null at row '{csvReader.Context.Parser.Row}', raw row content: '{csvReader.Context.Parser.RawRecord}'");

                return;
            }

            initializer?.Invoke(record);

            items.Add(record);
        }

        protected virtual object ReadRecord(CsvReader csvReader, Type recordType, ICsvContext csvContext)
        {
            try
            {
                return csvReader.GetRecord(recordType);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, $"Failed to read record of type '{recordType}'");

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }

            return null;
        }
        #endregion
    }
}
