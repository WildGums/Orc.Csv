// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumConverter.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2015 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using Catel;
    using global::CsvHelper.TypeConversion;

    /// <summary>
    /// Generic enum converter which can be used with CsvHelper
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    public class EnumConverter<T> : ITypeConverter 
        where T : struct, IComparable, IFormattable
    {
        private readonly T _defaultEnumValue;

        /// <summary>
        /// Constructor
        /// </summary>
        public EnumConverter()
            : this((T)Enum.ToObject(typeof(T), 0))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="defaultEnumValue">the default enum value which will be used if value cannot be parsed</param>
        public EnumConverter(T defaultEnumValue)
        {
            _defaultEnumValue = defaultEnumValue;
        }

        public string ConvertToString(TypeConverterOptions options, object value)
        {
            throw new NotImplementedException();
        }

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            T result;

            var success = Enum<T>.TryParse(text, true, out result);
            if (success)
            {
                return result;
            }

            return _defaultEnumValue;
        }

        public bool CanConvertFrom(Type type)
        {
            if (type == typeof(string))
            {
                return true;
            }

            return false;
        }

        public bool CanConvertTo(Type type)
        {
            throw new NotImplementedException();
        }
    }
}