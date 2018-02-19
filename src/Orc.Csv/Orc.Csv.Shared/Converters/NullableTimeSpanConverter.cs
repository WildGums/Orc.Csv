// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using CsvHelper;

    public class NullableTimeSpanConverter : NullableTypeConverterBase<TimeSpan?>
    {
        protected override TimeSpan? ConvertStringToActualType(IReaderRow row, string text)
        {
            if (!TimeSpan.TryParse(text, GetCultureInfo(row), out var value))
            {
                return null;
            }

            return value;
        }
    }
}