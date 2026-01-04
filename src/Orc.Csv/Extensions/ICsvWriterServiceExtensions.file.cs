namespace Orc.Csv;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using FileSystem;

public static partial class ICsvWriterServiceExtensions
{
    public static void WriteRecords(this ICsvWriterService csvWriterService, IFileService fileService, 
        IEnumerable records, string fileName, ICsvContext csvContext)
    {
        using (var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                csvWriterService.WriteRecords(records, streamWriter, csvContext);
            }
        }
    }

    public static async Task WriteRecordsAsync(this ICsvWriterService csvWriterService, IFileService fileService, 
        IEnumerable records, string fileName, ICsvContext csvContext)
    {
        using (var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                await csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
            }
        }
    }

    public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IFileService fileService, 
        IEnumerable<TRecord> records, string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        WriteRecords(csvWriterService, fileService, records, fileName, csvContext);
    }

    public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IFileService fileService, 
        IEnumerable<TRecord> records, string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        return WriteRecordsAsync(csvWriterService, fileService, records, fileName, csvContext);
    }

    public static CsvWriter CreateWriter(this ICsvWriterService csvWriterService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        // Note: don't dispose, the writer cannot be used when disposed
        var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
#pragma warning disable IDISP001 // Dispose created.
        var streamWriter = new StreamWriter(stream);
#pragma warning restore IDISP001 // Dispose created.
        return csvWriterService.CreateWriter(streamWriter, csvContext);
    }
}
