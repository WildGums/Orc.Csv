﻿namespace Orc.Csv.Tests.Converters
{
    using System.Collections.Generic;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class CustomAttributesTypeConverter : TypeConverterBase
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
            if (dictionary is null)
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
