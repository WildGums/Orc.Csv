namespace Orc.Csv
{
    using Catel.Reflection;
    using CsvHelper.Configuration;
    using System;

    public static class ClassMapExtensions
    {
        public static Type GetRecordType(this ClassMap classMap)
        {
            ArgumentNullException.ThrowIfNull(classMap);

            var requiredType = typeof(ClassMap<>);
            var type = classMap.GetType() ?? typeof(object);

            while (!type.IsGenericTypeEx() || type.GetGenericTypeDefinitionEx() != requiredType)
            {
                var subType = type.GetBaseTypeEx();
                if (subType is null)
                {
                    break;
                }

                type = subType;
            }

            return type.GetGenericArgumentsEx()[0];
        }
    }
}
