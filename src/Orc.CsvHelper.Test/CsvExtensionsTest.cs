// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExtensionsTest.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.CsvHelper.Test
{
    using System;
    using Csv;
    using Csv.Test.CsvMaps;
    using Csv.Test.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CsvExtensionsTest
    {
        #region Methods

        [TestMethod]
        public void FromCsvFile()
        {
            var result = CsvExtensions.FromCsvFile<Operation>(@"TestData\Operation.csv", typeof (OperationCsvMap));
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(1, result[1].Id);
            Assert.AreEqual("Operation2", result[1].Name);
            Assert.AreEqual(new DateTime(2014,5,30,20,30,0), result[1].StartTime);
            Assert.AreEqual(new TimeSpan(0,15,0), result[1].Duration);
            Assert.AreEqual(14.15, result[1].Quantity);
            Assert.AreEqual(true, result[1].Enabled);
        }

        #endregion
    }
}