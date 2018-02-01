// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


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
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region ICsvWriterService Members
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
            var configuration = EnsureCorrectConfiguration(csvContext.Configuration, csvContext.CultureInfo);

            var csvWriter = new CsvWriter(streamWriter, configuration);
            if (csvContext.ClassMap != null)
            {
                csvWriter.Configuration.RegisterClassMap(csvContext.ClassMap);
            }

            return csvWriter;
        }
        #endregion

        #region Methods
        protected virtual void WriteRecords(IEnumerable records, CsvWriter csvWriter, ICsvContext csvContext)
        {
            try
            {
                csvWriter.WriteHeader(csvContext.RecordType);
                csvWriter.WriteRecords(records);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Data["CsvHelper"].ToString();

                Log.Error("Cannot write row to csv. Error Details: {0}", errorMessage);

                if (csvContext.ThrowOnError)
                {
                    throw;
                }
            }
        }
        #endregion
    }
}