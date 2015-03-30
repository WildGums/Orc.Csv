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

            input = input.ToLower();
            if (input == "yes")
            {
                return true;
            }
            if (input == "no")
            {
                return false;
            }
            return false;
        }

    }
}