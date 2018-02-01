// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterServiceExtensions.file.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FileSystem;

    public static partial class ICsvWriterServiceExtensions
    {
        public static void WriteRecords(this ICsvWriterService csvWriterService, IEnumerable records, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvWriterService);

            var dependencyResolver = csvWriterService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            using (var stream = fileService.OpenWrite(fileName))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    csvWriterService.WriteRecords(records, streamWriter, csvContext);
                }
            }
        }

        public static async Task WriteRecordsAsync(this ICsvWriterService csvWriterService, IEnumerable records, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvWriterService);

            var dependencyResolver = csvWriterService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            using (var stream = fileService.OpenWrite(fileName))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    await csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
                }
            }
        }

        public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string fileName, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvWriterService);

            if (csvContext == null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            WriteRecords(csvWriterService, records, fileName, csvContext);
        }

        public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string fileName, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvWriterService);

            if (csvContext == null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return WriteRecordsAsync(csvWriterService, records, fileName, csvContext);
        }

        public static CsvWriter CreateWriter(this ICsvWriterService csvWriterService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvWriterService);

            var dependencyResolver = csvWriterService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            // Note: don't dispose, the writer cannot be used when disposed
            var stream = fileService.OpenWrite(fileName);
            var streamWriter = new StreamWriter(stream);
            return csvWriterService.CreateWriter(streamWriter, csvContext);
        }
    }
}