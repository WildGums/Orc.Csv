namespace Orc.Csv;

using System;
using System.Globalization;
using CsvHelper.Configuration;

public interface ICsvContext
{
    Type RecordType { get; set; }
    CultureInfo? Culture { get; set; }
    CsvConfiguration? Configuration { get; set; }
    Action<object>? Initializer { get; set; }
    bool ThrowOnError { get; set; }
    ClassMap? ClassMap { get; set; }
}