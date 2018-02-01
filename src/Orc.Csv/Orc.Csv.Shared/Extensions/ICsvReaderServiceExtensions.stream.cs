// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FileSystem;

    public static partial class ICsvReaderServiceExtensions
    {
        #region Methods
        public static List<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = csvReaderService.ReadRecords(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static async Task<List<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static List<TRecord> ReadRecords<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvReaderService);

            if (csvContext == null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return ReadRecords<TRecord>(csvReaderService, streamReader, csvContext);
        }

        public static Task<List<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvReaderService);

            if (csvContext == null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return ReadRecordsAsync<TRecord>(csvReaderService, streamReader, csvContext);
        }
        #endregion
    }
}