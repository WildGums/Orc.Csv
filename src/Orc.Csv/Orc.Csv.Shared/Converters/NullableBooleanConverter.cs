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
        public NullableBooleanConverter()
        {
            TrueValues = new List<string>(new [] { "yes", "1", "on", "true" });
            FalseValues = new List<string>(new[] { "no", "0", "off", "false" });
        }

        public List<string> TrueValues { get; private set; }

        public List<string> FalseValues { get; private set; }

        protected override bool? ConvertStringToActualType(IReaderRow row, string text)
        {
            if (text.EqualsAnyIgnoreCase(TrueValues.ToArray()))
            {
                return true;
            }

            if (text.EqualsAnyIgnoreCase(FalseValues.ToArray()))
            {
                return false;
            }

            return false;
        }
    }
}