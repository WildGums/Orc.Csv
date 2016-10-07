// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YesNoToBooleanConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;

    public class YesNoToBooleanConverter : TypeConverter<bool>
    {
        public YesNoToBooleanConverter()
            : base(ConvertFromString)
        {
        }

        public YesNoToBooleanConverter(string defaultInput)
            : base(ConvertFromString, defaultInput)
        {
            throw new NotImplementedException();
        }

        private static bool ConvertFromString(string s1, string s2)
        {
            throw new NotImplementedException();
        }

        private static bool ConvertFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (string.Equals(input,"yes", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            if (string.Equals(input, "no", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return false;
        }
    }
}