// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvReaderService.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    public interface ICsvReaderService
    {
        #region Methods
        IEnumerable ReadRecords(StreamReader streamReader, ICsvContext csvContext);

        Task<IEnumerable> ReadRecordsAsync(StreamReader streamReader, ICsvContext csvContext);

        CsvReader CreateReader(StreamReader streamReader, ICsvContext csvContext);

        Configuration CreateDefaultConfiguration(CultureInfo cultureInfo = null);
        #endregion
    }
}