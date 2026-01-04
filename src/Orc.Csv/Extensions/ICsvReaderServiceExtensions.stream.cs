namespace Orc.Csv;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catel;
using CsvHelper.Configuration;

public static partial class ICsvReaderServiceExtensions
{
    public static IReadOnlyList<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
    {
        Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

        var records = csvReaderService.ReadRecords(streamReader, csvContext);
        return records.Cast<TRecord>().ToArray();
    }

    public static async Task<IReadOnlyList<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
    {
        Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

        var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
        return records.Cast<TRecord>().ToArray();
    }

    public static IReadOnlyList<TRecord> ReadRecords<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        return ReadRecords<TRecord>(csvReaderService, streamReader, csvContext);
    }

    public static Task<IReadOnlyList<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        return ReadRecordsAsync<TRecord>(csvReaderService, streamReader, csvContext);
    }
}
