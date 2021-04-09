// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterServiceExtensions.stream.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using CsvHelper.Configuration;

    public static partial class ICsvWriterServiceExtensions
    {
        #region Methods
        public static void WriteRecords<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, StreamWriter streamWriter, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvWriterService);

            if (csvContext is null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            csvWriterService.WriteRecords(records, streamWriter, csvContext);
        }

        public static Task WriteRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, StreamWriter streamWriter, ICsvContext csvContext = null)
            where TRecordMap : ClassMap, new()
        {
            Argument.IsNotNull(() => csvWriterService);

            if (csvContext is null)
            {
                csvContext = new CsvContext<TRecord, TRecordMap>();
            }

            return csvWriterService.WriteRecordsAsync(records, streamWriter, csvContext);
        }
        #endregion
    }
}