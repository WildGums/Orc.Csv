// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper;
    using FileSystem;

    public static class ICsvReaderServiceExtensions
    {
        #region Methods
        public static List<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = csvReaderService.ReadRecords(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static async Task<List<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, StreamReader streamReader, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static IEnumerable ReadRecords(this ICsvReaderService csvReaderService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);

            var dependencyResolver = csvReaderService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            using (var stream = fileService.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var records = csvReaderService.ReadRecords(streamReader, csvContext);
                    return records;
                }
            }
        }

        public static async Task<IEnumerable> ReadRecordsAsync(this ICsvReaderService csvReaderService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);

            var dependencyResolver = csvReaderService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            using (var stream = fileService.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var records = await csvReaderService.ReadRecordsAsync(streamReader, csvContext);
                    return records;
                }
            }
        }

        public static List<TRecord> ReadRecords<TRecord>(this ICsvReaderService csvReaderService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = csvReaderService.ReadRecords(fileName, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static async Task<List<TRecord>> ReadRecordsAsync<TRecord>(this ICsvReaderService csvReaderService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsOfType("csvContext.RecordType", csvContext.RecordType, typeof(TRecord));

            var records = await csvReaderService.ReadRecordsAsync(fileName, csvContext);
            return records.Cast<TRecord>().ToList();
        }

        public static CsvReader CreateReader(this ICsvReaderService csvReaderService, string fileName, ICsvContext csvContext)
        {
            Argument.IsNotNull(() => csvReaderService);

            var dependencyResolver = csvReaderService.GetDependencyResolver();
            var fileService = dependencyResolver.Resolve<IFileService>();

            // Note: don't dispose, the reader cannot be used when disposed
            var stream = fileService.OpenRead(fileName);
            var streamReader = new StreamReader(stream);
            return csvReaderService.CreateReader(streamReader, csvContext);
        }
        #endregion
    }
}