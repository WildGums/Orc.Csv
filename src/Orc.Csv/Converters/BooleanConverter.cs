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
            // Dummy collection
            FalseValues = new List<string>();

            _trueValuesList.AddRange(new [] { "yes", "1", "on", "true", "y", "t" });
        }

        public BooleanConverter(string[] trueValues)
            : this()
        {
            if (trueValues != null)
            {
                // Replace the list
                _trueValuesList.Clear();
                _trueValuesList.AddRange(trueValues);
            }
        }

        [ObsoleteEx(ReplacementTypeOrMember = "BooleanConverter(string[])", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "4.0")]
        public BooleanConverter(string[] trueValues, string[] falseValues)
            : this(trueValues)
        {
            // No implementation required
        }

        public BooleanConverter AddTrueValues(params string[] values)
        {
            if (values != null)
            {
                _trueValuesList.AddRange(values);
                _trueValues = null;
            }

            return this;
        }

        [ObsoleteEx(ReplacementTypeOrMember = "AddTrueValues", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "4.0")]
        public List<string> TrueValues
        {
            get
            {
                // Always reset so we refresh
                _trueValues = null;

                return _trueValuesList;
            }
        }

        [ObsoleteEx(Message = "False values are not necessary in non-nullable boolean conversions (default value = false)", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "4.0")]
        public List<string> FalseValues { get; private set; }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            text = text.Trim();

            if (_trueValues == null)
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
