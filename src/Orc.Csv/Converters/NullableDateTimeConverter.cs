// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableDateTimeConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using CsvHelper;
    using CsvHelper.TypeConversion;

    public class NullableDateTimeConverter : NullableTypeConverterBase<DateTime?>
    {
        protected override DateTime? ConvertStringToActualType(IReaderRow row, string text)
        {
            var value = Convert.ToDateTime(text, GetCultureInfo(row));
            if (value == CsvEnvironment.ExcelNullDate)
            {
                return null;
            }

            return value;
        }
    }
}