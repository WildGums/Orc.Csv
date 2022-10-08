namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using CsvHelper.Configuration;

    public static partial class ICsvReaderServiceExtensions
    {
        public static List<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            ArgumentNullException.ThrowIfNull(csvReaderService);
            ArgumentNullException.ThrowIfNull(streamReader);
            ArgumentNullException.ThrowIfNull(csvContext);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = csvReaderService.ReadRecords(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static async Task<List<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            ArgumentNullException.ThrowIfNull(csvReaderService);
            ArgumentNullException.ThrowIfNull(streamReader);
            ArgumentNullException.ThrowIfNull(csvContext);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static List<TRecord> ReadRecords<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext? csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            ArgumentNullException.ThrowIfNull(csvReaderService);
            ArgumentNullException.ThrowIfNull(streamReader);

            if (csvContext is null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return ReadRecords<TRecord>(csvReaderService, streamReader, csvContext);
        }

        public static Task<List<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext? csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            ArgumentNullException.ThrowIfNull(csvReaderService);
            ArgumentNullException.ThrowIfNull(streamReader);

            if (csvContext is null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return ReadRecordsAsync<TRecord>(csvReaderService, streamReader, csvContext);
        }
    }
}
