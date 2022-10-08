namespace Orc.Csv
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Catel.IoC;
    using CsvHelper.Configuration;

    internal static class ITypeFactoryExtensions
    {
        public static bool TryToCreateClassMap(this ITypeFactory typeFactory, Type type, [NotNullWhen(true)]out ClassMap? classMap)
        {
            ArgumentNullException.ThrowIfNull(typeFactory);
            ArgumentNullException.ThrowIfNull(type);

            classMap = typeFactory.CreateInstanceWithParametersAndAutoCompletion(type) as ClassMap;
            return classMap is not null;
        }
    }
}
