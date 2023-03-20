namespace Orc.Csv;

using System;
using System.Globalization;
using CsvHelper.Configuration;

public class CsvContext : ICsvContext
{
    public CsvContext(Type recordType)
    {
        ArgumentNullException.ThrowIfNull(recordType);

        RecordType = recordType;
        ThrowOnError = true;
    }

    public Type RecordType { get; set; }

    public ClassMap? ClassMap { get; set; }

    public CsvConfiguration? Configuration { get; set; }

    public CultureInfo? Culture { get; set; }

    public Action<object>? Initializer { get; set; }

    public bool ThrowOnError { get; set; }
}

public class CsvContext<TRecord> : CsvContext
{
    public CsvContext()
        : base(typeof(TRecord))
    {

    }
}

public class CsvContext<TRecord, TMap> : CsvContext<TRecord>
    where TMap : ClassMap, new()
{
    public CsvContext()
        : base()
    {
        ClassMap = new TMap();
    }
}