// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableTypeConverterBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using CsvHelper.TypeConversion;

    public abstract class StringToNullableTypeConverterBase<TNullable> : TypeConverterBase<TNullable>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var value = ConvertStringToActualType(options, text);
            return value;
        }

        /// <summary>
        /// Converts the string to the actual type. The null checks are already performed.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        protected abstract TNullable ConvertStringToActualType(TypeConverterOptions options, string text);
    }
}