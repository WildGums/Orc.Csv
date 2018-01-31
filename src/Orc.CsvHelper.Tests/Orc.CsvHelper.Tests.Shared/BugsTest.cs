// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BugsTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.CsvHelper.Tests
{
    using Catel.IO;
    using Csv;
    using FileSystem;
    using global::CsvHelper.Configuration;
    using NUnit.Framework;

    [TestFixture]
    public class BugsTest
    {
        private static readonly string TestInputFolder = Path.Combine(AssemblyDirectoryHelper.GetCurrentDirectory(), @"TestData\");

        [Test]
        public void GetFieldByColumnName_NoExceptionsShouldBeThrown()
        {
            // Arrange
            var csvFilePath = $"{TestInputFolder}{"Operation.csv"}";

            var csvReaderService = new CsvReaderService(new FileService());
            var configuration = new global::CsvHelper.Configuration.Configuration
            {
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim
            };

            using (var csvReader = ICsvReaderServiceExtensions.CreateReader(csvReaderService, csvFilePath, configuration: configuration))
            {
                while (csvReader.Read())
                {
                    var id = csvReader.GetField("Id");
                    var name = csvReader.GetField("Name");
                }
            }
        }
    }
}