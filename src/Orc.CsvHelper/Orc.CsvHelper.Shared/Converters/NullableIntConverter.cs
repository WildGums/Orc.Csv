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

    public class NullableIntConverter : NullableTypeConverterBase<int?>
    {
        protected override int? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToInt32(text, GetCultureInfo(row));
            return value;
        }
    }
}