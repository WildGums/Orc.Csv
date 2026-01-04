namespace Orc.Csv.Tests
{
    using Catel;
    using Microsoft.Extensions.DependencyInjection;
    using Orc.Csv;
    using Orc.FileSystem;

    internal static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddCatelCore();
            serviceCollection.AddOrcFileSystem();
            serviceCollection.AddOrcCsv();

            return serviceCollection;
        }
    }
}
