// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using CsvHelper.TypeConversion;
    using System;

    public class StringToNullableDoubleConverter : StringToNullableTypeConverterBase<double?>
    {
        protected override double? ConvertStringToActualType(TypeConverterOptions options, string text)
        {
            var value = Convert.ToDouble(text, options.CultureInfo);
            if (double.IsNaN(value))
            {
                return null;
            }

            return value;
        }
    }
}