// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvContext.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Globalization;
    using CsvHelper.Configuration;

    public interface ICsvContext
    {
        #region Properties
        Type RecordType { get; set; }
        [ObsoleteEx(ReplacementTypeOrMember = "Culture", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "3.1")]
        CultureInfo CultureInfo { get; set; }
        CultureInfo Culture { get; set; }
        Configuration Configuration { get; set; }
        Action<object> Initializer { get; set; }
        bool ThrowOnError { get; set; }
        ClassMap ClassMap { get; set; }
        #endregion
    }
}