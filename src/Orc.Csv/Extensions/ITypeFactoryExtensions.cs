namespace Orc.Csv;

using System;
using System.Diagnostics.CodeAnalysis;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class ITypeFactoryExtensions
{
    public static bool TryToCreateClassMap(this IServiceProvider serviceProvider, Type type, [NotNullWhen(true)]out ClassMap? classMap)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(type);

        classMap = ActivatorUtilities.CreateInstance(serviceProvider, type) as ClassMap;
        return classMap is not null;
    }
}
