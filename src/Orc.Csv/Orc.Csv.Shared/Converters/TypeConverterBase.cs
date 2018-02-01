// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Globalization;
    using CsvHelper;
    using CsvHelper.Configuration;
    using global::CsvHelper.TypeConversion;

    public abstract class TypeConverterBase<T> : ITypeConverter
    {
        public virtual string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var stringValue = Convert.ToString(value, GetCultureInfo(row));
            return stringValue;
        }

        public abstract object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData);

        protected CultureInfo GetCultureInfo(IWriterRow row)
        {
            var culture = row?.Configuration?.CultureInfo ?? CsvEnvironment.DefaultCultureInfo;
            return culture;
        }

        protected CultureInfo GetCultureInfo(IReaderRow row)
        {
            var culture = row?.Configuration?.CultureInfo ?? CsvEnvironment.DefaultCultureInfo;
            return culture;
        }
    }
}