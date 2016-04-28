// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGenerationTest.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
	using NUnit.Framework;

	[TestFixture]
	public class CodeGenerationTest
	{
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
			Assert.Fail();
		}

		[Test]
		public void GetCsvFilesTest()
		{
			Assert.Fail();
		}

		[Test]
		public void CreateCSharpFilesTest()
		{
			Assert.Fail();
		}


		[Test]
		public void ToCamelCaseTest()
		{
			Assert.Fail();
		}
	}
}