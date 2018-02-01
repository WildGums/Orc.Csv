// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableDecimalConverter : NullableTypeConverterBase<decimal?>
    {
        protected override decimal? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToDecimal(text, GetCultureInfo(row));
            return value;
        }
    }
}