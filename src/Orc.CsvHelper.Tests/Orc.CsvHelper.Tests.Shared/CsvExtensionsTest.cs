// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensionsTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.IoC;
    using CsvHelper.Tests;
    using CsvMaps;
    using Entities;
    using NUnit.Framework;
    using Path = Catel.IO.Path;

    [TestFixture]
    public class CsvExtensionsTest
    {
        [Test]
        public async Task ToCsvFileAsync()
        {
            var serviceLocator = ServiceLocator.Default;
            var csvWriterService = serviceLocator.ResolveType<ICsvWriterService>();

            var csvFilePath = System.IO.Path.GetTempFileName();
            var originalData = CreateSampleOperations(60).ToList();
            await csvWriterService.WriteCsvAsync(originalData, csvFilePath);

            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            var result = csvReaderService.ReadCsv<Operation, OperationCsvMap>(csvFilePath);

            var expectedEnumerator = result.GetEnumerator();
            foreach (var operation in result)
            {
                expectedEnumerator.MoveNext();
                var expectedOperation = expectedEnumerator.Current;

                Assert.AreEqual(operation.Id, expectedOperation.Id);
                Assert.AreEqual(operation.Name, expectedOperation.Name);
                Assert.AreEqual(operation.StartTime, expectedOperation.StartTime);
                Assert.AreEqual(operation.Duration, expectedOperation.Duration);
                Assert.AreEqual(operation.Quantity, expectedOperation.Quantity);
                Assert.AreEqual(operation.Enabled, expectedOperation.Enabled);
            }

            if (File.Exists(csvFilePath))
            {
                File.Delete(csvFilePath);
            }
        }

        [Test]
        public void FromCsvFile()
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();

            var result = csvReaderService.ReadCsv<Operation, OperationCsvMap>(Path.Combine(AssemblyDirectoryHelper.GetCurrentDirectory(), @"TestData\Operations.csv"));
            var expectedResult = CreateSampleOperations(5);

            var expectedEnumerator = expectedResult.GetEnumerator();
            foreach (var operation in result)
            {
                expectedEnumerator.MoveNext();
                var expectedOperation = expectedEnumerator.Current;

                Assert.AreEqual(operation.Id, expectedOperation.Id);
                Assert.AreEqual(operation.Name, expectedOperation.Name);
                Assert.AreEqual(operation.StartTime, expectedOperation.StartTime);
                Assert.AreEqual(operation.Duration, expectedOperation.Duration);
                Assert.AreEqual(operation.Quantity, expectedOperation.Quantity);
                Assert.AreEqual(operation.Enabled, expectedOperation.Enabled);
            }
        }

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
    }
}