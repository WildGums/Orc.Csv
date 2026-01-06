namespace Orc
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.Csv;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcCsvModule
    {
        public static IServiceCollection AddOrcCsv(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<ICsvReaderService, CsvReaderService>();
            serviceCollection.TryAddSingleton<ICsvWriterService, CsvWriterService>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.Csv", "Orc.Csv.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.Csv", "https://github.com/wildgums/orc.csv"));
            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new ResourceBasedThirdPartyNotice("CsvHelper", "https://joshclose.github.io/CsvHelper/", "Orc.Csv", "Orc.Csv", "Resources.ThirdPartyNotices.csvhelper.txt"));

            return serviceCollection;
        }
    }
}
