// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvServiceBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System.Globalization;
    using CsvHelper.Configuration;

    public abstract class CsvServiceBase
    {
        public virtual Configuration CreateDefaultConfiguration(CultureInfo cultureInfo)
        {
            var configuration = new Configuration
            {
                CultureInfo = cultureInfo ?? CsvEnvironment.DefaultCultureInfo,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
            };

            return configuration;
        }

        protected Configuration EnsureCorrectConfiguration(Configuration configuration, CultureInfo cultureInfo)
        {
            if (configuration != null && cultureInfo != null)
            {
                configuration.CultureInfo = cultureInfo;
            }

            return configuration ?? CreateDefaultConfiguration(cultureInfo);
        }
    }
}