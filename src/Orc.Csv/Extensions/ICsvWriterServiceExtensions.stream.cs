namespace Orc.Csv;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper.Configuration;

public static partial class ICsvWriterServiceExtensions
{
    public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, StreamWriter streamWriter, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(streamWriter);

        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        csvWriterService.WriteRecords(records, streamWriter, csvContext);
    }

    public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, StreamWriter streamWriter, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(streamWriter);

        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        return csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
    }
}
