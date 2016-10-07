// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationCsvMap.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.CsvMaps
{
    using Entities;
    using global::CsvHelper.Configuration;

    public class OperationCsvMap : CsvClassMap<Operation>
    {
        #region Constructors
        public OperationCsvMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Name).Name("Name");
            Map(x => x.StartTime).Name("StartTime");
            Map(x => x.Duration).Name("Duration");
            Map(x => x.Quantity).Name("Quantity");
            Map(x => x.Enabled).Name("Enabled");
        }
        #endregion
    }
}