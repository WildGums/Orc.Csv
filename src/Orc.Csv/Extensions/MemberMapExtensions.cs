namespace Orc.Csv
{
    using System;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    public static class MemberMapExtensions
    {
        public static MemberMap AsString(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            // Dummy wrapper
            return map.Default(string.Empty);
        }

        public static MemberMap AsNullableString(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableStringConverter>();
        }

        public static MemberMap AsEnum<TEnum>(this MemberMap map, TEnum defaultValue = default)
            where TEnum : struct, IComparable, IFormattable
        {
            ArgumentNullException.ThrowIfNull(map);

            var enumConverter = new EnumConverter<TEnum>(defaultValue);
            return map.TypeConverter(enumConverter);
        }

        public static MemberMap AsDateTime(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<DateTimeConverter>();
        }

        public static MemberMap AsNullableDateTime(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableDateTimeConverter>();
        }

        public static MemberMap AsTimeSpan(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<TimeSpanConverter>();
        }

        public static MemberMap AsNullableTimeSpan(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableTimeSpanConverter>();
        }

        public static MemberMap AsDouble(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<DoubleConverter>();
        }

        public static MemberMap AsNullableDouble(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableDoubleConverter>();
        }

        public static MemberMap AsDecimal(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<DecimalConverter>();
        }

        public static MemberMap AsNullableDecimal(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableDecimalConverter>();
        }

        public static MemberMap AsShort(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<Int16Converter>();
        }

        public static MemberMap AsNullableShort(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableShortConverter>();
        }

        public static MemberMap AsInt(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<Int32Converter>();
        }

        public static MemberMap AsNullableInt(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableIntConverter>();
        }

        public static MemberMap AsLong(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<Int64Converter>();
        }

        public static MemberMap AsNullableLong(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableLongConverter>();
        }

        public static MemberMap AsBool(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<BooleanConverter>();
        }

        public static MemberMap AsBool(this MemberMap map, string[] additionalTrueValues)
        {
            ArgumentNullException.ThrowIfNull(map);

            var typeConverter = new BooleanConverter()
                .AddTrueValues(additionalTrueValues);

            return map.TypeConverter(typeConverter);
        }

        public static MemberMap AsNullableBool(this MemberMap map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return map.TypeConverter<NullableBooleanConverter>();
        }

        public static MemberMap AsNullableBool(this MemberMap map, string[] additionalTrueValues, string[] additionalFalseValues)
        {
            ArgumentNullException.ThrowIfNull(map);

            var typeConverter = new NullableBooleanConverter()
                .AddTrueValues(additionalTrueValues)
                .AddFalseValues(additionalFalseValues);

            return map.TypeConverter(typeConverter);
        }
    }
}
