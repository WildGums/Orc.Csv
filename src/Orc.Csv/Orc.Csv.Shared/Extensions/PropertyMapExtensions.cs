// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvHelperExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    internal static class CsvHelperExtensions
    {
        #region Methods
        public static MemberMap AsDateTime(this MemberMap map)
        {
            return map.TypeConverter<DateTimeConverter>();
        }

        public static MemberMap AsNullableDateTime(this MemberMap map)
        {
            return map.TypeConverter<NullableDateTimeConverter>();
        }

        public static MemberMap AsDouble(this MemberMap map)
        {
            return map.TypeConverter<DoubleConverter>();
        }

        public static MemberMap AsNullableDouble(this MemberMap map)
        {
            return map.TypeConverter<NullableDoubleConverter>();
        }

        public static MemberMap AsDecimal(this MemberMap map)
        {
            return map.TypeConverter<DecimalConverter>();
        }

        public static MemberMap AsNullableDecimal(this MemberMap map)
        {
            return map.TypeConverter<NullableDecimalConverter>();
        }

        public static MemberMap AsShort(this MemberMap map)
        {
            return map.TypeConverter<Int16Converter>();
        }

        public static MemberMap AsNullableShort(this MemberMap map)
        {
            return map.TypeConverter<NullableShortConverter>();
        }

        public static MemberMap AsInt(this MemberMap map)
        {
            return map.TypeConverter<Int32Converter>();
        }

        public static MemberMap AsNullableInt(this MemberMap map)
        {
            return map.TypeConverter<NullableIntConverter>();
        }

        public static MemberMap AsLong(this MemberMap map)
        {
            return map.TypeConverter<Int64Converter>();
        }

        public static MemberMap AsNullableLong(this MemberMap map)
        {
            return map.TypeConverter<NullableLongConverter>();
        }

        public static MemberMap AsBool(this MemberMap map)
        {
            return map.TypeConverter<BooleanConverter>();
        }

        public static MemberMap AsNullableBool(this MemberMap map)
        {
            return map.TypeConverter<NullableBooleanConverter>();
        }
        #endregion
    }
}