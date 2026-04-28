namespace Orc.Csv;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catel;
using CsvHelper;
using CsvHelper.Configuration;
using FileSystem;

public static partial class ICsvReaderServiceExtensions
{
    public static IEnumerable ReadRecords(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        using var stream = fileService.OpenRead(fileName);
        using var streamReader = new StreamReader(stream);
        var records = csvReaderService.ReadRecords(streamReader, csvContext);
        return records;
    }

    public static async Task<IEnumerable> ReadRecordsAsync(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        using var stream = fileService.OpenRead(fileName);
        using var streamReader = new StreamReader(stream);
        var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
        return records;
    }

    public static IReadOnlyList<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

        var records = csvReaderService.ReadRecords(fileService, fileName, csvContext);
        return records.Cast<TRecord>().ToList();
    }

    public static async Task<IReadOnlyList<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

        var records = await csvReaderService.ReadRecordsAsync(fileService, fileName, csvContext);
        return records.Cast<TRecord>().ToList();
    }

    public static IReadOnlyList<TRecord> ReadRecords<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        return ReadRecords<TRecord>(csvReaderService, fileService, fileName, csvContext);
    }

    public static Task<IReadOnlyList<TRecord>> ReadRecordsAsync<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext? csvContext = null)
        where TRecordMap : ClassMap, new()
    {
        csvContext ??= new CsvContext<TRecord, TRecordMap>();

        return ReadRecordsAsync<TRecord>(csvReaderService, fileService, fileName, csvContext);
    }

    public static CsvReader CreateReader(this ICsvReaderService csvReaderService, IFileService fileService, 
        string fileName, ICsvContext csvContext)
    {
        // Note: don't dispose, the reader cannot be used when disposed
        var stream = fileService.OpenRead(fileName);
#pragma warning disable IDISP001 // Dispose created.
        var streamReader = new StreamReader(stream);
#pragma warning restore IDISP001 // Dispose created.
        return csvReaderService.CreateReader(streamReader, csvContext);
    }
}
