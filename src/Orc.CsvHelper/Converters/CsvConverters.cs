// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvConverters.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;

    public static class CsvConverters
    {
        #region Constants
        public static readonly TypeConverter<DateTime?> StringToNullableDateTime = new TypeConverter<DateTime?>(ConvertFromStringToNullableDateTime);
        #endregion

        #region Methods
        public static DateTime? ConvertFromStringToNullableDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var dateTime = Convert.ToDateTime(input);
            if (dateTime == CsvExtensions.ExcelNullDate)
            {
                return null;
            }

            return dateTime;
        }
        #endregion
    }
}