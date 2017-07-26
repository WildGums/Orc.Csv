// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationServiceTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Csv.Tests
{
    using Catel.IO;
    using CsvHelper.Tests;
    using FileSystem;
    using NUnit.Framework;

	[TestFixture]
	public class ValidationServiceTest
	{
		private static readonly string TestInputFolder = Path.Combine(AssemblyDirectoryHelper.GetCurrentDirectory(), @"TestData\");

        [Test]
		[TestCase("Operation.csv", 0, 0)]
		[TestCase("2Operation.csv", 1, 0)]
		[TestCase("Operation2Invalid.csv", 0, 2)]
		[TestCase("Operation2Duplicate.csv", 0, 2)]
		[TestCase("OperationWithMissingColumn.csv", 0, 0)]
		public void CreateCSharpFilesTest(string fileName, int businessRuleErrorCount, int fieldErrorCount)
		{
			// Arrange
			var csvFilePath = $"{TestInputFolder}{fileName}";

			// Act
			var service = new CsvValidationService(new EntityPluralService(), new CsvReaderService(new FileService()));
			var result = service.Validate(csvFilePath);

			// Assert:
			Assert.AreEqual(businessRuleErrorCount, result.GetBusinessRuleErrorCount());
			Assert.AreEqual(fieldErrorCount, result.GetFieldErrorCount());
		}
	}
}