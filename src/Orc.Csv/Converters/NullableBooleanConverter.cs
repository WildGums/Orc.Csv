namespace Orc.Csv
{
    using System.Collections.Generic;
    using Catel;
    using CsvHelper;

    public class NullableBooleanConverter : NullableTypeConverterBase<bool?>
    {
        private readonly List<string> _trueValuesList = new List<string>();
        private string[]? _trueValues;

        private readonly List<string> _falseValuesList = new List<string>();
        private string[]? _falseValues;

        public NullableBooleanConverter()
        {
            _trueValuesList.AddRange(new [] { "yes", "1", "on", "true" });
            _falseValuesList.AddRange(new[] { "no", "0", "off", "false" });
        }

        public NullableBooleanConverter(string[]? trueValues, string[]? falseValues)
            : this()
        {
            if (trueValues is not null)
            {
                // Replace the list
                _trueValuesList.Clear();
                _trueValuesList.AddRange(trueValues);
            }

            if (falseValues is not null)
            {
                // Replace the list
                _falseValuesList.Clear();
                _falseValuesList.AddRange(falseValues);
            }
        }

        public NullableBooleanConverter AddTrueValues(params string[] values)
        {
            if (values is not null)
            {
                _trueValuesList.AddRange(values);
                _trueValues = null;
            }

            return this;
        }

        public NullableBooleanConverter AddFalseValues(params string[] values)
        {
            if (values is not null)
            {
                _falseValuesList.AddRange(values);
                _falseValues = null;
            }

            return this;
        }

        protected override bool? ConvertStringToActualType(IReaderRow row, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
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

            if (_falseValues is null)
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
