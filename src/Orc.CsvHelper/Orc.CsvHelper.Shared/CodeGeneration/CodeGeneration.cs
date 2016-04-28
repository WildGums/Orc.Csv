// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGeneration.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Csv
{
	using System.Collections.Generic;
	using System.Data.Entity.Infrastructure.Pluralization;
	using System.IO;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Create CSharp files to consume CSV files.
	/// A standard POCO cs file as well as the CsvHelper Mapping cs file will be created.
	/// All properties in the POCO will be of type string. So please update accordingly.
	/// </summary>
	public static class CodeGeneration
	{
		private static readonly IPluralizationService PluralizationService = new EnglishPluralizationService();

		public static void CreateCSharpFilesForAllCsvFiles(string inputFoler, string namespaceName, string outputFolder)
		{
			var csvFiles = GetCsvFiles(inputFoler);

			foreach (var csvFile in csvFiles)
			{
				CreateCSharpFiles(csvFile, namespaceName, outputFolder);
			}
		}

		public static string[] GetCsvFiles(string folderPath)
		{
			return Directory.GetFiles(folderPath, "*.csv");
		}

		public static void CreateCSharpFiles(string csvFilePath, string namespaceName, string outputFolder)
		{
			var fileName = Path.GetFileNameWithoutExtension(csvFilePath);
			var className = ToCamelCase(fileName);

			var csvReader = CsvReaderHelper.CreateReader(csvFilePath);

			var properties = new List<string>();
			var propertyMaps = new List<string>();

			var getSet = "{ get; set; }";

			csvReader.Read();

			foreach (var fieldHeader in csvReader.FieldHeaders)
			{
				var propertyName = ToCamelCase(fieldHeader);
				properties.Add($"public string {propertyName} {getSet}");
				propertyMaps.Add($"Map(x => x.{propertyName}).Name(\"{fieldHeader}\");");
			}

			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}

			CreateCSharpPocoFile(namespaceName, className, properties, outputFolder);
			CreateCSharpCsvMapFile(namespaceName, className, propertyMaps, outputFolder);
		}

		private static void CreateCSharpPocoFile(string namespaceName, string className, IEnumerable<string> properties, string outputFolder)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"namespace {namespaceName}");
			sb.AppendLine("{");
			sb.AppendLine($"{Spaces(4)}using System;");
			sb.AppendLine("");
			sb.AppendLine($"{Spaces(4)}public class {className}");
			sb.AppendLine(Spaces(4) + "{");
			foreach (var property in properties)
			{
				sb.AppendLine($"{Spaces(8)}{property}");
			}
			sb.AppendLine(Spaces(4) + "}");
			sb.Append("}");

			var content = sb.ToString();
			var filePath = Path.Combine(outputFolder, className + ".cs");

			File.WriteAllText(filePath, content);
		}

		private static void CreateCSharpCsvMapFile(string namespaceName, string className, IEnumerable<string> properties, string outputFolder)
		{
			var classNameMap = className + "Map";
			var sb = new StringBuilder();

			sb.AppendLine($"namespace {namespaceName}");
			sb.AppendLine("{");
			sb.AppendLine($"{Spaces(4)}using CsvHelper.Configuration;");
			sb.AppendLine("");
			sb.AppendLine($"{Spaces(4)}public sealed class {classNameMap} : CsvClassMap<{className}>");
			sb.AppendLine(Spaces(4) + "{");
			sb.AppendLine($"{Spaces(8)}public {classNameMap}()");
			sb.AppendLine(Spaces(8) + "{");
			foreach (var property in properties)
			{
				sb.AppendLine($"{Spaces(16)}{property}");
			}
			sb.AppendLine(Spaces(8) + "}");
			sb.AppendLine(Spaces(4) + "}");
			sb.AppendLine("}");

			var content = sb.ToString();
			var filePath = Path.Combine(outputFolder, classNameMap + ".cs");

			File.WriteAllText(filePath, content);
		}

		public static string ToSingular(this string input)
		{
			// TODO: Implement handling invalid characters and other cases like starting with number
			// Currently this is only a KISS singularize transform to statisfy SolutionGenerator's needs
			return PluralizationService.Singularize(input);
		}

		public static string ToCamelCase(this string input)
		{
			// Remove all not alphanumeric characters. Leave white spaces.
			var cleanChars = input.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))).ToArray();

			var words = new string(cleanChars).Split(' ');
			var sb = new StringBuilder();

			foreach (var word in words)
			{
				if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(word))
				{
					continue;
				}

				var firstLetter = word.Substring(0, 1).ToUpper();
				var rest = word.Substring(1, word.Length - 1);
				sb.Append(firstLetter + rest);
			}

			return sb.ToString();
		}

		private static string Spaces(int n)
		{
			return new string(' ', n);
		}
	}
}