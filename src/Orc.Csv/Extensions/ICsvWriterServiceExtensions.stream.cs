namespace Orc.Csv;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper.Configuration;

public static partial class ICsvWriterServiceExtensions
{
    public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IReadOnlyList<TRecord> records, StreamWriter streamWriter, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        csvWriterService.WriteRecords(records, streamWriter, csvContext);
    }

    public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IReadOnlyList<TRecord> records, StreamWriter streamWriter, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        return csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
    }
}
