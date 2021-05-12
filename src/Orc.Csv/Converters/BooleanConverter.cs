// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YesNoToBooleanConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Collections.Generic;
    using Catel;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class BooleanConverter : TypeConverterBase
    {
        private readonly List<string> _trueValuesList = new List<string>();
        private string[] _trueValues;

        public BooleanConverter()
        {
            _trueValuesList.AddRange(new [] { "yes", "1", "on", "true", "y", "t" });
        }

        public BooleanConverter(string[] trueValues)
            : this()
        {
            if (trueValues is not null)
            {
                // Replace the list
                _trueValuesList.Clear();
                _trueValuesList.AddRange(trueValues);
            }
        }

        public BooleanConverter AddTrueValues(params string[] values)
        {
            if (values is not null)
            {
                _trueValuesList.AddRange(values);
                _trueValues = null;
            }

            return this;
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            text = text.Trim();

            if (_trueValues is null)
            {
                _trueValues = _trueValuesList.ToArray();
            }

            if (text.EqualsAnyIgnoreCase(_trueValues))
            {
                return true;
            }

            // Note: no need to check for false values (default value is false)

            return false;
        }
    }
}
