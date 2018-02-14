// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YesNoToBooleanConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class BooleanConverter : TypeConverterBase<bool>
    {
        public BooleanConverter()
        {
            TrueValues = new List<string>(new [] { "yes", "1", "on", "true", "y", "t" });
            FalseValues = new List<string>(new[] { "no", "0", "off", "false", "n", "f" });
        }

        public BooleanConverter(string[] additionalTrueValues, string[] additionalFalseValues)
            : this()
        {
            if (additionalTrueValues != null)
            {
                TrueValues.AddRange(additionalTrueValues);
            }

            if (additionalFalseValues != null)
            {
                FalseValues.AddRange(additionalFalseValues);
            }
        }

        public List<string> TrueValues { get; private set; }

        public List<string> FalseValues { get; private set; }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            text = text.Trim();

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