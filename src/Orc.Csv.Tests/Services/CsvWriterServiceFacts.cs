namespace Orc.Csv.Tests.Services;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvMaps;
using Entities;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Orc.FileSystem;
using VerifyNUnit;

[TestFixture]
public class CsvWriterServiceFacts
{
    [Test]
    public async Task WritesWithCustomAttributeConvertersAsync()
    {
        var fileService = new FileService(NullLogger<FileService>.Instance);

        var writerService = new CsvWriterService(NullLogger<CsvWriterService>.Instance);

        var attributes = new List<CustomAttribute>();

        for (var i = 0; i < 3; i++)
        {
            attributes.Add(new CustomAttribute
            {
                Value = $"Attribute{i + 1}"
            });
        }

        var operations = new List<Operation>();

        for (var i = 0; i < 5; i++)
        {
            var operation = new Operation
            {
                Id = i + 1,
                Name = $"Operation {i + 1}",
                Enabled = true
            };

            for (var j = 0; j < 5; j++)
            {
                operation.Attributes[$"Attribute{j + 1}"] = $"Value {j + 1}";
            }

            operations.Add(operation);
        }

        using var temporaryFileContext = new TemporaryFilesContext($"{nameof(CsvWriterServiceFacts)}_{nameof(WritesWithCustomAttributeConvertersAsync)}");
        var fileName = temporaryFileContext.GetFile("operations.csv");

        var classMap = new OperationMap();
        classMap.Initialize(attributes.Select(x => x.Value));

        var csvContext = new CsvContext<Operation>
        {
            ClassMap = classMap,
            Culture = new System.Globalization.CultureInfo("nl-NL")
        };

        csvContext.Culture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

        await using (var stream = File.Create(fileName))
        {
            await using (var textWriter = new StreamWriter(stream))
            {
                await using (var csvWriter = new CsvWriter(textWriter, new CsvConfiguration(csvContext.Culture)))
                {
                    csvWriter.Context.RegisterClassMap(classMap);

                    await csvWriter.WriteRecordsAsync(operations);
                }
            }
        }

        await writerService.WriteRecordsAsync(fileService, operations, fileName, csvContext);

        await Verifier.VerifyFile(fileName);
    }

    [Test]
    public async Task WritesHeaderForEmptyRecordSetAsync()
    {
        var fileService = new FileService(NullLogger<FileService>.Instance);

        var writerService = new CsvWriterService(NullLogger<CsvWriterService>.Instance);

        var operations = new List<Operation>();

        using var temporaryFileContext = new TemporaryFilesContext($"{nameof(CsvWriterServiceFacts)}_{nameof(WritesHeaderForEmptyRecordSetAsync)}");
        var fileName = temporaryFileContext.GetFile("operations.csv");

        var csvContext = new CsvContext<Operation>();

        await writerService.WriteRecordsAsync(fileService, operations, fileName, csvContext);

        await Verifier.VerifyFile(fileName);
    }
}
