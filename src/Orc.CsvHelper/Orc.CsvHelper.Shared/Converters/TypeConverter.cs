// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using global::CsvHelper.TypeConversion;

    public class TypeConverter<T> : ITypeConverter
    {
        #region Fields
        private readonly Func<string, string, T> _convertFromStringWithDefaultValueFunc;
        private readonly string _defaultInput;
        private readonly Func<string, T> _convertFromStringFunc;
        #endregion

        #region Constructors
        public TypeConverter(Func<string, T> convertFromString)
        {
            _convertFromStringFunc = convertFromString;
        }

        public TypeConverter(Func<string, string, T> convertFromStringWithDefaultValue, string defaultInput)
        {
            _convertFromStringWithDefaultValueFunc = convertFromStringWithDefaultValue;
            _defaultInput = defaultInput;
        }
        #endregion

        #region ITypeConverter Members
        /// <summary>
        /// Note: No conversion is undertaken. The value is simply returned as a string.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return value.ToString();
        }

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (_convertFromStringFunc != null)
            {
                return _convertFromStringFunc(text);
            }

            if (_convertFromStringWithDefaultValueFunc != null)
            {
                return _convertFromStringWithDefaultValueFunc(text, _defaultInput);
            }

            throw new Exception();
        }

        public bool CanConvertFrom(Type type)
        {
            if (type == typeof (string))
            {
                return true;
            }

            return false;
        }

        public bool CanConvertTo(Type type)
        {
            return true;
        }
        #endregion
    }
}