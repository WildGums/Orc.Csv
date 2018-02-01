// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using CsvHelper;
    using FileSystem;

    public static class ICsvWriterServiceExtensions
    {
        #region Methods
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
        #endregion
    }
}