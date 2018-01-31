// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using global::CsvHelper.TypeConversion;

    public abstract class TypeConverterBase<T> : ITypeConverter
    {
        #region Constructors
        public TypeConverterBase()
        {
        }
        #endregion

        #region ITypeConverter Members
        /// <summary>
        /// Note: No conversion is undertaken. The value is simply returned as a string.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string ConvertToString(TypeConverterOptions options, object value)
        {
            return value.ToString();
        }

        public abstract object ConvertFromString(TypeConverterOptions options, string text);

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
            return true;
        }
        #endregion
    }
}