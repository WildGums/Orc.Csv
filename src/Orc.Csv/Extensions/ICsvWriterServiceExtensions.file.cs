namespace Orc.Csv;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Catel.IoC;
using CsvHelper;
using CsvHelper.Configuration;
using FileSystem;

public static partial class ICsvWriterServiceExtensions
{
    public static void WriteRecords(this ICsvWriterService csvWriterService, IEnumerable records, string fileName, ICsvContext csvContext)
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(fileName);
        ArgumentNullException.ThrowIfNull(csvContext);

        var dependencyResolver = csvWriterService.GetDependencyResolver();
        var fileService = dependencyResolver.ResolveRequired<IFileService>();

        using (var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                csvWriterService.WriteRecords(records, streamWriter, csvContext);
            }
        }
    }

    public static async Task WriteRecordsAsync(this ICsvWriterService csvWriterService, IEnumerable records, string fileName, ICsvContext csvContext)
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(fileName);
        ArgumentNullException.ThrowIfNull(csvContext);

        var dependencyResolver = csvWriterService.GetDependencyResolver();
        var fileService = dependencyResolver.ResolveRequired<IFileService>();

        using (var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                await csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
            }
        }
    }

    public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(fileName);

        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        WriteRecords(csvWriterService, records, fileName, csvContext);
    }

    public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(fileName);

        if (csvContext is null)
        {
            csvContext = new CsvContext<TRecord, TRecordMap>();
        }

        return WriteRecordsAsync(csvWriterService, records, fileName, csvContext);
    }

    public static CsvWriter CreateWriter(this ICsvWriterService csvWriterService, string fileName, ICsvContext csvContext)
    {
        ArgumentNullException.ThrowIfNull(csvWriterService);
        ArgumentNullException.ThrowIfNull(fileName);
        ArgumentNullException.ThrowIfNull(csvContext);

        var dependencyResolver = csvWriterService.GetDependencyResolver();
        var fileService = dependencyResolver.ResolveRequired<IFileService>();

        // Note: don't dispose, the writer cannot be used when disposed
        var stream = fileService.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
#pragma warning disable IDISP001 // Dispose created.
        var streamWriter = new StreamWriter(stream);
#pragma warning restore IDISP001 // Dispose created.
        return csvWriterService.CreateWriter(streamWriter, csvContext);
    }
}