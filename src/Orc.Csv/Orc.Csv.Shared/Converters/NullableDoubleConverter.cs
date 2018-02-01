// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using CsvHelper.TypeConversion;
    using System;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class NullableDoubleConverter : NullableTypeConverterBase<double?>
    {
        protected override double? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToDouble(text, GetCultureInfo(row));
            if (double.IsNaN(value))
            {
                return null;
            }

            return value;
        }
    }
}