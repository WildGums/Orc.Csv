// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvContext.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Globalization;
    using Catel;
    using CsvHelper.Configuration;

    public class CsvContext : ICsvContext
    {
        public CsvContext(Type recordType)
        {
            Argument.IsNotNull(() => recordType);

            RecordType = recordType;
            ThrowOnError = true;
        }

        public Type RecordType { get; set; }

        public ClassMap ClassMap { get; set; }

        public Configuration Configuration { get; set; }

        [ObsoleteEx(ReplacementTypeOrMember = "Culture", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "3.1")]
        public CultureInfo CultureInfo
        {
            get { return Culture; }
            set { Culture = value; }
        }

        public CultureInfo Culture { get; set; }

        public Action<object> Initializer { get; set; }

        public bool ThrowOnError { get; set; }
    }

    public class CsvContext<TRecord> : CsvContext
    {
        public CsvContext()
         : base(typeof(TRecord))
        {

        }
    }

    public class CsvContext<TRecord, TMap> : CsvContext<TRecord>
        where TMap : ClassMap, new()
    {
        public CsvContext()
        {
            ClassMap = new TMap();
        }
    }
}