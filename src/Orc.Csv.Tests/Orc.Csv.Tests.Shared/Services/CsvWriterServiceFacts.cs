// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvWriterServiceFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2018 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApprovalTests;
    using CsvMaps;
    using Entities;
    using NUnit.Framework;

    [TestFixture]
    public class CsvWriterServiceFacts
    {
        [Test]
        public async Task WritesWithCustomAttributeConvertersAsync()
        {
            var writerService = new CsvWriterService();

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

            var temporaryFileContext = new TemporaryFilesContext($"{nameof(CsvWriterServiceFacts)}_{nameof(WritesWithCustomAttributeConvertersAsync)}");
            var fileName = temporaryFileContext.GetFile("operations.csv");

            var classMap = new OperationMap();
            classMap.Initialize(attributes.Select(x => x.Value));

            var csvContext = new CsvContext<Operation>
            {
                ClassMap = classMap
            };

            await writerService.WriteRecordsAsync(operations, fileName, csvContext);

            Approvals.VerifyFile(fileName);
        }
    }
}