namespace Orc.Csv
{
    using System.Collections;
    using System.IO;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvWriterService
    {
        void WriteRecords(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext);

        Task WriteRecordsAsync(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext);

        CsvWriter CreateWriter(StreamWriter streamWriter, ICsvContext csvContext);

        CsvConfiguration CreateDefaultConfiguration(ICsvContext csvContext);
    }
}
