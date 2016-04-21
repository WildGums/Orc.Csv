// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGeneration.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Orc.Csv
{
    using System;
    using System.Collections.Generic;
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
                properties.Add(string.Format("public string {0} {1}", propertyName, getSet));
                propertyMaps.Add(string.Format("Map(x => x.{0}).Name(\"{1}\");", propertyName, fieldHeader));
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

            sb.AppendLine(string.Format("namespace {0}", namespaceName));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("{0}using System;", Spaces(4)));
            sb.AppendLine("");
            sb.AppendLine(string.Format("{0}public class {1}", Spaces(4), className));
            sb.AppendLine(Spaces(4) + "{");
            foreach (var property in properties)
            {
                sb.AppendLine(string.Format("{0}{1}", Spaces(8), property));
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

            sb.AppendLine(string.Format("namespace {0}", namespaceName));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("{0}using CsvHelper.Configuration;", Spaces(4)));
            sb.AppendLine("");
            sb.AppendLine(string.Format("{0}public sealed class {1} : CsvClassMap<{2}>", Spaces(4), classNameMap, className));
            sb.AppendLine(Spaces(4) + "{");
            sb.AppendLine(string.Format("{0}public {1}()", Spaces(8), classNameMap));
            sb.AppendLine(Spaces(8) + "{");
            foreach (var property in properties)
            {
                sb.AppendLine(string.Format("{0}{1}", Spaces(16), property));
            }
            sb.AppendLine(Spaces(8) + "}");
            sb.AppendLine(Spaces(4) + "}");
            sb.AppendLine("}");

            var content = sb.ToString();
            var filePath = Path.Combine(outputFolder, classNameMap + ".cs");

            File.WriteAllText(filePath, content);
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