// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGenerationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Csv
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Catel;
    using Catel.Logging;

    /// <summary>
    /// Create CSharp files to consume CSV files.
    /// A standard POCO cs file as well as the CsvHelper Mapping cs file will be created.
    /// All properties in the POCO will be of type string. So please update accordingly.
    /// </summary>
    public class CodeGenerationService : ICodeGenerationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IEntityPluralService _entityPluralService;

        public CodeGenerationService(IEntityPluralService entityPluralService)
        {
            Argument.IsNotNull(() => entityPluralService);

            _entityPluralService = entityPluralService;
        }

        public void CreateCSharpFilesForAllCsvFiles(string inputFoler, string namespaceName, string outputFolder)
        {
            var csvFiles = GetCsvFiles(inputFoler);

            foreach (var csvFile in csvFiles)
            {
                CreateCSharpFiles(csvFile, namespaceName, outputFolder);
            }
        }

        public string[] GetCsvFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath, "*.csv");
        }

        public void CreateCSharpFiles(string csvFilePath, string namespaceName, string outputFolder)
        {
            var fileName = Path.GetFileNameWithoutExtension(csvFilePath);
            var className = fileName.ToCamelCase();
            className = _entityPluralService.ToSingular(className);

            var csvReader = CsvReaderHelper.CreateReader(csvFilePath);

            var properties = new List<string>();
            var propertyMaps = new List<string>();

            var getSet = "{ get; set; }";

            csvReader.Read();

            for (int index = 0; index < csvReader.FieldHeaders.Length; index++)
            {
                var fieldHeader = csvReader.FieldHeaders[index];
                if (string.IsNullOrEmpty(fieldHeader))
                {
                    Log.Warning($"Skipping column #{index} in data file {fileName} because its column header is missing.");
                    continue;
                }

                var propertyName = fieldHeader.ToCamelCase();
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

        private void CreateCSharpPocoFile(string namespaceName, string className, IEnumerable<string> properties, string outputFolder)
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

        private void CreateCSharpCsvMapFile(string namespaceName, string className, IEnumerable<string> properties, string outputFolder)
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

        private static string Spaces(int n)
        {
            return new string(' ', n);
        }
    }
}