// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2015 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;

    public class StringToNullableDateTimeConverter : TypeConverter<DateTime?>
    {
        #region Constructors
        public StringToNullableDateTimeConverter()
            : base(ConvertFromString)
        {
        }

        public StringToNullableDateTimeConverter(string defaultInput)
            : base(ConvertFromString, defaultInput)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods
        private static DateTime? ConvertFromString(string s1, string s2)
        {
            throw new NotImplementedException();
        }

        private static DateTime? ConvertFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var dateTime = Convert.ToDateTime(input);
            if (dateTime == CsvEnvironment.ExcelNullDate)
            {
                return null;
            }

            return dateTime;
        }
        #endregion
    }
}