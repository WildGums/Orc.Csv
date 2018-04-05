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

    public class NullableBooleanConverter : NullableTypeConverterBase<bool?>
    {
        private List<string> _trueValuesList = new List<string>();
        private string[] _trueValues;

        private List<string> _falseValuesList = new List<string>();
        private string[] _falseValues;

        public NullableBooleanConverter()
        {
            _trueValuesList.AddRange(new [] { "yes", "1", "on", "true" });
            _falseValuesList.AddRange(new[] { "no", "0", "off", "false" });
        }

        public NullableBooleanConverter(string[] trueValues, string[] falseValues)
            : this()
        {
            if (trueValues != null)
            {
                // Replace the list
                _trueValuesList.Clear();
                _trueValuesList.AddRange(trueValues);
            }

            if (falseValues != null)
            {
                // Replace the list
                _falseValuesList.Clear();
                _falseValuesList.AddRange(falseValues);
            }
        }

        public NullableBooleanConverter AddTrueValues(params string[] values)
        {
            _trueValuesList.AddRange(values);
            _trueValues = null;

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

        public NullableBooleanConverter AddFalseValues(params string[] values)
        {
            _falseValuesList.AddRange(values);
            _falseValues = null;

            return this;
        }

        [ObsoleteEx(ReplacementTypeOrMember = "AddFalseValues", TreatAsErrorFromVersion = "3.0", RemoveInVersion = "4.0")]
        public List<string> FalseValues
        {
            get
            {
                // Always reset so we refresh
                _falseValues = null;

                return _falseValuesList;
            }
        }

        protected override bool? ConvertStringToActualType(IReaderRow row, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
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

            if (_falseValues == null)
            {
                _falseValues = _falseValuesList.ToArray();
            }

            if (text.EqualsAnyIgnoreCase(_falseValues))
            {
                return false;
            }

            return false;
        }
    }
}