// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvWriterService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvWriterService
    {
        #region Methods
        void WriteRecords(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext);

        Task WriteRecordsAsync(IEnumerable records, StreamWriter streamWriter, ICsvContext csvContext);

        CsvWriter CreateWriter(StreamWriter streamWriter, ICsvContext csvContext);

        Configuration CreateDefaultConfiguration(CultureInfo cultureInfo = null);
        #endregion
    }
}