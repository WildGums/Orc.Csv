namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Threading.Tasks;
    using Catel.Logging;
    using CsvHelper;

    public class CsvWriterService : CsvServiceBase, ICsvWriterService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public virtual void WriteRecords(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext)
        {
            using (var csvWriter = CreateWriter(streamWriter, csvContext))
            {
                WriteRecords(records, csvWriter, csvContext);

                csvWriter.Flush();
            }
        }

        public virtual async Task WriteRecordsAsync(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext)
        {
            using (var csvWriter = CreateWriter(streamWriter, csvContext))
            {
                WriteRecords(records, csvWriter, csvContext);

                await csvWriter.FlushAsync();
            }
        }

        public virtual CsvWriter CreateWriter(StreamWriter streamWriter, ICsvContext csvContext)
        {
            var configuration = EnsureCorrectConfiguration(csvContext.Configuration, csvContext);
            var csvWriter = new CsvWriter(streamWriter, configuration);

            if (csvContext.ClassMap is not null)
            {
                csvWriter.Context.RegisterClassMap(csvContext.ClassMap);
            }

            return csvWriter;
        }

        protected virtual void WriteRecords(IEnumerable records, CsvWriter csvWriter, ICsvContext csvContext)
        {
            try
            {
                // Note: no need to write the header, the WriteRecords method will take care of that.

                Log.Debug($"Writing records");

                var enumerator = records.GetEnumerator();
                if (!enumerator.MoveNext() && (csvContext.Configuration?.HasHeaderRecord??true))
                {
                    // 0 records, only write header
                    csvWriter.WriteHeader(csvContext.RecordType);
                }
                else
                {
                    // Note: one would expect that we need to write enumerator.Current first (since we already touched
                    // it), but it doesn't seem necessary
                    csvWriter.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to write data");

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }
        }
    }
}
