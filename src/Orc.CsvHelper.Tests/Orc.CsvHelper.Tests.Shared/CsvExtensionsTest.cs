// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensionsTest.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Csv;
    using Entities;
    using CsvMaps;
    using NUnit.Framework;

    [TestFixture]
    public class CsvExtensionsTest
    {
        #region Methods
        [Test]
        public void FromCsvFile()
        {
            var result = CsvReaderHelper.ReadCsv<Operation, OperationCsvMap>(@"TestData\Operation.csv");
            var expectedResult = CreateSampleOperations();

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

        [Test]
        public void ToCsvFile()
        {
            var file = @"Operation.csv";
            var operations = CreateSampleOperations();
            operations.ToCsv(file, typeof (OperationCsvMap));

            var resultCsvLines = File.ReadAllLines(file) as IEnumerable<string>;

            var expectedCsvLines = CreateSampleCsv();
            AssertCollectionsAreEqual(expectedCsvLines, resultCsvLines);

            // clean up
            File.Delete(file);
        }
        #endregion

        #region Helpers
        private void AssertCollectionsAreEqual(IEnumerable<string> expectedCsvLines, IEnumerable<string> resultCsvLines)
        {
            var iterator = resultCsvLines.GetEnumerator();
            foreach (var expectedCsvLine in expectedCsvLines)
            {
                iterator.MoveNext();
                var resultCsvLine = iterator.Current;
                Assert.AreEqual(expectedCsvLine, resultCsvLine);
            }
        }

        private IEnumerable<string> CreateSampleCsv()
        {
            yield return @"Id,Name,StartTime,Duration,Quantity,Enabled";
            yield return @"0,Operation1,30/05/2014 7:30:00 PM,00:00:00,14,False";
            yield return @"1,Operation2,30/05/2014 8:30:00 PM,00:15:00,14.15,True";
            yield return @"2,Operation3,30/05/2014 9:30:00 PM,00:30:00,14.3,False";
            yield return @"3,Operation4,30/05/2014 10:30:00 PM,00:45:00,14.45,True";
            yield return @"4,Operation5,30/05/2014 10:30:00 PM,01:00:00,14.6,False";
        }

        private IEnumerable<Operation> CreateSampleOperations()
        {
            yield return new Operation
            {
                Id = 0,
                Name = "Operation1",
                StartTime = new DateTime(2014, 5, 30, 19, 30, 0),
                Duration = new TimeSpan(0, 0, 0),
                Quantity = 14.0,
                Enabled = false,
            };

            yield return new Operation
            {
                Id = 1,
                Name = "Operation2",
                StartTime = new DateTime(2014, 5, 30, 20, 30, 0),
                Duration = new TimeSpan(0, 15, 0),
                Quantity = 14.15,
                Enabled = true,
            };

            yield return new Operation
            {
                Id = 2,
                Name = "Operation3",
                StartTime = new DateTime(2014, 5, 30, 21, 30, 0),
                Duration = new TimeSpan(0, 30, 0),
                Quantity = 14.3,
                Enabled = false,
            };

            yield return new Operation
            {
                Id = 3,
                Name = "Operation4",
                StartTime = new DateTime(2014, 5, 30, 22, 30, 0),
                Duration = new TimeSpan(0, 45, 0),
                Quantity = 14.45,
                Enabled = true,
            };

            yield return new Operation
            {
                Id = 4,
                Name = "Operation5",
                StartTime = new DateTime(2014, 5, 30, 22, 30, 0),
                Duration = new TimeSpan(1, 0, 0),
                Quantity = 14.60,
                Enabled = false,
            };
        }
        #endregion
    }
}