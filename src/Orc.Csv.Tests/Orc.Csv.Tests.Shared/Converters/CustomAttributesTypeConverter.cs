// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAttributesTypeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.Converters
{
    using System.Collections.Generic;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class CustomAttributesTypeConverter : TypeConverterBase<object>
    {
        #region Fields
        private readonly string _customAttribute;
        #endregion

        #region Constructors
        public CustomAttributesTypeConverter(string customAttribute)
        {
            _customAttribute = customAttribute;
        }
        #endregion

        #region Methods
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var dictionary = value as Dictionary<string, string>;
            if (dictionary == null)
            {
                return string.Empty;
            }

            var result = dictionary[_customAttribute];
            return result ?? string.Empty;
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}