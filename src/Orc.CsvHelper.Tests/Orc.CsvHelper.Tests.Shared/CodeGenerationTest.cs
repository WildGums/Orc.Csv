// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGenerationTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
	using System;
	using System.IO;
	using NUnit.Framework;

	[TestFixture]
	public class CodeGenerationTest
	{
		private const string TestInputFolder = @"TestData\";
		private const string ExpectedFolder = @"Expected\";

		[TestCase("Parts", "Part")]
		[TestCase("Entities", "Entity")]
		[TestCase("Feet", "Foot")]
		[TestCase("Men", "Man")]
		[TestCase("Women", "Woman")]
		[TestCase("People", "Person")]
		public void ToSingularTest(string input, string expected)
		{
			// Singularize
			Assert.AreEqual(expected, input.ToSingular());
			// Check if does not harm an already singular form:
			Assert.AreEqual(expected, expected.ToSingular());
		}

		[Test]
		public void CreateCSharpFilesForAllCsvFilesTest()
		{
			//Assert.Fail();
		}

		[Test]
		public void GetCsvFilesTest()
		{
			//Assert.Fail();
		}

		[Test]
		[TestCase("Operation.csv", "Operation.cs", "OperationMap.cs")]
		[TestCase("Operations.csv", "Operation.cs", "OperationMap.cs")]
		[TestCase("OperationWithMissingColumn.csv", "OperationWithMissingColumn.cs", "OperationWithMissingColumnMap.cs")]
		public void CreateCSharpFilesTest(string fileName, string modelClassFileName, string mapClassFileName)
		{
			// Arrange
			var namespaceName = "nameSpace";
			var csvFilePath = $"{TestInputFolder}{fileName}";
			var outputFolder = $"{Path.GetTempPath()}{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}";
			Directory.CreateDirectory(outputFolder);

			// Act
			CodeGeneration.CreateCSharpFiles(csvFilePath, namespaceName, outputFolder);

			// Assert:
			Assert.AreEqual(2, Directory.GetFiles(outputFolder, "*.*").Length);
			AssertFilesAreEqual($"{ExpectedFolder}\\{modelClassFileName}", $"{outputFolder}\\{modelClassFileName}");
			AssertFilesAreEqual($"{ExpectedFolder}{mapClassFileName}", $"{outputFolder}\\{mapClassFileName}");

			Directory.Delete(outputFolder, true);
		}

		private void AssertFilesAreEqual(string expected, string actual)
		{
			var s = File.ReadAllText(expected);
			var readAllText = File.ReadAllText(actual);
			Assert.AreEqual(s, readAllText);
		}

		[Test]
		public void ToCamelCaseTest() 
		{
			//Assert.Fail();
		}
	}
}