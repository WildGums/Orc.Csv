// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using CsvHelper.TypeConversion;

    public class StringToNullableDateTimeConverter : StringToNullableTypeConverterBase<DateTime?>
    {
        protected override DateTime? ConvertStringToActualType(TypeConverterOptions options, string text)
        {
            var value = Convert.ToDateTime(text, options.CultureInfo);
            if (value == CsvEnvironment.ExcelNullDate)
            {
                return null;
            }

            return value;
        }
    }
}