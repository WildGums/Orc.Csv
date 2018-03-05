// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationCsvMap.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.CsvMaps
{
    using System.Collections.Generic;
    using Catel.Reflection;
    using Converters;
    using Entities;
    using global::CsvHelper.Configuration;

    public class OperationMap : ClassMap<Operation>
    {
        #region Constructors
        public OperationMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Name).Name("Name");
            Map(x => x.StartTime).Name("StartTime");
            Map(x => x.Duration).Name("Duration");
            Map(x => x.Quantity).Name("Quantity");
            Map(x => x.Enabled).Name("Enabled");
        }
        #endregion

        public void Initialize(IEnumerable<string> customAttributes)
        {
            var classType = typeof(Operation);

            var customAttributesPropertyInfo = classType.GetPropertyEx("Attributes");
            foreach (var customAttribute in customAttributes)
            {
                var csvPropertyMap = MemberMap.CreateGeneric(classType, customAttributesPropertyInfo);
                csvPropertyMap.Name(customAttribute).TypeConverter(new CustomAttributesTypeConverter(customAttribute));

                MemberMaps.Add(csvPropertyMap);
            }
        }
    }
}