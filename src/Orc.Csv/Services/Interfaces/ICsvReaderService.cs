namespace Orc.Csv;

using System.Collections;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

public interface ICsvReaderService
{
    IEnumerable ReadRecords(StreamReader streamReader, ICsvContext csvContext);

    Task<IEnumerable> ReadRecordsAsync(StreamReader streamReader, ICsvContext csvContext);

    CsvReader CreateReader(StreamReader streamReader, ICsvContext csvContext);

    CsvConfiguration CreateDefaultConfiguration(ICsvContext csvContext);
}