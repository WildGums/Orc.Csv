// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNullableTypeConverterBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using Catel;
    using CsvHelper;
    using CsvHelper.Configuration;

    public abstract class NullableTypeConverterBase<TNullable> : TypeConverterBase
    {
        public NullableTypeConverterBase()
        {
            SupportNullText = true;
        }

        /// <summary>
        /// If set the <c>true</c>, the text <c>null</c> will be treated as null as well.
        /// <para />
        /// The default value is <c>true</c>.
        /// </summary>
        public bool SupportNullText { get; set; }

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

            if (SupportNullText && text.EqualsIgnoreCase("null"))
            {
                return null;
            }

            var nullValues = memberMapData?.TypeConverterOptions?.NullValues;
            if (nullValues?.Contains(text)??false)
            {
                return null;
            }

            var value = ConvertStringToActualType(row, text);
            return value;
        }

        /// <summary>
        ///     Converts the string to the actual type. The null checks are already performed.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        protected abstract TNullable ConvertStringToActualType(IReaderRow row, string text);
        #endregion
    }
}
