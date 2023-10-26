namespace Orc.Csv.Tests.Services;

using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Orc.Csv.Tests.CsvMaps;
using Orc.FileSystem;

[TestFixture]
public class CsvReaderServiceFacts
{
    private static readonly string TestInputFolder = Path.Combine(AssemblyDirectoryHelper.GetCurrentDirectory(), @"TestData\");

    [Test]
    public async Task Test_Async()
    {
        var csvFilePath = $"{TestInputFolder}UserDefinedFieldTypes.csv";
        var csvReaderService = new CsvReaderService();

        var records = await csvReaderService.ReadRecordsAsync<UserDefinedFieldType, UserDefinedFieldTypeMap>(csvFilePath);
    }
}
