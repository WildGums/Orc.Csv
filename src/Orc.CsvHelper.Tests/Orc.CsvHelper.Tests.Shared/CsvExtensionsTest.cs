// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensionsTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.IoC;
    using Catel.IO;
    using CsvHelper.Tests;
    using CsvMaps;
    using Entities;
    using NUnit.Framework;

    [TestFixture]
    public class CsvExtensionsTest
    {
        #region Helpers
        private IEnumerable<Operation> CreateSampleOperations(int count)
        {
            var prototypeOperation = new Operation
            {
                Id = 0,
                Name = "Operation",
                StartTime = new DateTime(2014, 5, 30, 19, 30, 0),
                Duration = new TimeSpan(0, 0, 0),
                Quantity = 14.0,
                Enabled = false,
            };

            for (var i = 0; i < count; i++)
            {
                var operation = prototypeOperation.Clone();
                operation.Id = i;
                operation.Name += i;
                operation.StartTime += new TimeSpan(0, i, 0);
                operation.Duration += new TimeSpan(0, 15 * i, 0);
                operation.Quantity += 0.15 * i;
                operation.Enabled = i % 2 == 0;

                yield return operation;
            }
        }
        #endregion

        [Test]
        public async Task ToCsvFileAsync()
        {
            var serviceLocator = ServiceLocator.Default;
            var typeFactory = serviceLocator.ResolveType<ITypeFactory>();
            var csvWriterService = typeFactory.CreateInstanceWithParametersAndAutoCompletion<CsvWriterService>();
            var csvReaderService = typeFactory.CreateInstanceWithParametersAndAutoCompletion<CsvReaderService>();

            using (var tempFilesContext = new TemporaryFilesContext("CsvWriterService"))
            {
                var csvFilePath = tempFilesContext.GetFile("CsvWriterServiceTest.csv");
                var originalData = CreateSampleOperations(5).ToList();
                await csvWriterService.WriteCsvAsync(originalData, csvFilePath);

                var result = ICsvReaderServiceExtensions.ReadCsv<Operation, OperationCsvMap>(csvReaderService, csvFilePath);

                var operationCounter = 0;
                using (var expectedEnumerator = originalData.GetEnumerator())
                {
                    foreach (var operation in result)
                    {
                        expectedEnumerator.MoveNext();
                        var expectedOperation = expectedEnumerator.Current;
                        operationCounter++;

                        Assert.AreEqual(operation.Id, expectedOperation.Id);
                        Assert.AreEqual(operation.Name, expectedOperation.Name);
                        Assert.AreEqual(operation.StartTime, expectedOperation.StartTime);
                        Assert.AreEqual(operation.Duration, expectedOperation.Duration);
                        Assert.AreEqual(operation.Quantity, expectedOperation.Quantity);
                        Assert.AreEqual(operation.Enabled, expectedOperation.Enabled);
                    }
                }

                Assert.AreEqual(operationCounter, originalData.Count);
            }
        }

        [Test]
        public async Task FromCsvFileAsync()
        {
            var serviceLocator = ServiceLocator.Default;
            var typeFactory = serviceLocator.ResolveType<ITypeFactory>();
            var csvReaderService = typeFactory.CreateInstanceWithParametersAndAutoCompletion<CsvReaderService>();
            var testDataDirectory = AssemblyDirectoryHelper.GetTestDataDirectory();
            var csvFileName = "Operations.csv";
            var csvFilePath = Path.Combine(testDataDirectory, csvFileName);

            var result = await csvReaderService.ReadCsvAsync<Operation, OperationCsvMap>(csvFilePath);

            var expectedResult = CreateSampleOperations(5);
            var operationCounter = 0;
            using (var expectedEnumerator = expectedResult.GetEnumerator())
            {
                foreach (var operation in result)
                {
                    expectedEnumerator.MoveNext();
                    var expectedOperation = expectedEnumerator.Current;
                    operationCounter++;

                    Assert.AreEqual(operation.Id, expectedOperation.Id);
                    Assert.AreEqual(operation.Name, expectedOperation.Name);
                    Assert.AreEqual(operation.StartTime, expectedOperation.StartTime);
                    Assert.AreEqual(operation.Duration, expectedOperation.Duration);
                    Assert.AreEqual(operation.Quantity, expectedOperation.Quantity);
                    Assert.AreEqual(operation.Enabled, expectedOperation.Enabled);
                }
            }

            Assert.AreEqual(operationCounter, result.Count);
        }
    }
}