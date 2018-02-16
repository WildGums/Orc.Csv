// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableTypeConverterBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using CsvHelper;
    using CsvHelper.Configuration;

    public abstract class NullableTypeConverterBase<TNullable> : TypeConverterBase<TNullable>
    {
        #region Methods
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // Note: we could (should?) respect the trim option here, but since we really want to convert types,
            // we need to trim
            text = text.Trim();

            var value = ConvertStringToActualType(row, text);
            return value;
        }

        /// <summary>
        /// Converts the string to the actual type. The null checks are already performed.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        protected abstract TNullable ConvertStringToActualType(IReaderRow row, string text);
        #endregion
    }
}