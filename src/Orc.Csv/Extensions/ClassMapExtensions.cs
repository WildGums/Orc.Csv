// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassMapExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using Catel.Reflection;
    using CsvHelper.Configuration;
    using System;

    public static class ClassMapExtensions
    {
        public static Type GetRecordType(this ClassMap classMap)
        {
            var requiredType = typeof(ClassMap<>);
            var type = classMap.GetType();

            while (!type.IsGenericTypeEx() || type.GetGenericTypeDefinitionEx() != requiredType)
            {
                type = type.BaseType;
            }

            return type.GetGenericArgumentsEx()[0];
        }
    }
}