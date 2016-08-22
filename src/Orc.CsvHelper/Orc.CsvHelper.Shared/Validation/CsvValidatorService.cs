// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvValidatorService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
	using System.CodeDom.Compiler;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Catel;
	using Catel.Data;
	using Catel.Logging;

	public class CsvValidationService : ICsvValidationService
	{
		#region Constants
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();
		#endregion

		#region Fields
		private readonly IEntityPluralService _pluralService;
		#endregion

		#region Constructors
		public CsvValidationService(IEntityPluralService pluralService)
		{
			Argument.IsNotNull(() => pluralService);
			_pluralService = pluralService;
		}
		#endregion

		#region ICsvValidationService Members
		public IValidationContext Validate(string csvFilePath)
		{
			var className = _pluralService.ToSingular(Path.GetFileNameWithoutExtension(csvFilePath).ToCamelCase());
			var csvReader = CsvReaderHelper.CreateReader(csvFilePath);
			csvReader.Read();

			var propertyNames =
				from fieldHeader in csvReader.FieldHeaders
				where !string.IsNullOrEmpty(fieldHeader)
				select fieldHeader.ToCamelCase();

			return Validate(csvFilePath, className, propertyNames);
		}

		public IValidationContext Validate(string csvFilePath, string className, IEnumerable<string> propertyNames)
		{
			var result = new ValidationContext();

			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(className))
			{
				result.AddBusinessRuleValidationResult(new BusinessRuleValidationResult(ValidationResultType.Error, $"Inferred class name '{className}' is invalid. File: {csvFilePath}"));
			}
			var propertyNameList = propertyNames.ToList();

			foreach (var propertyName in propertyNameList)
			{
				if (!CodeGenerator.IsValidLanguageIndependentIdentifier(propertyName))
				{
					result.AddFieldValidationResult(new FieldValidationResult(propertyName, ValidationResultType.Error, $"Inferred property name '{propertyName}' is invalid. File: {csvFilePath}"));
				}
			}

			var duplicates = propertyNameList
				.GroupBy(x => x)
				.Where(group => group.Count() > 1)
				.Select(group => group.Key);

			foreach (var duplicate in duplicates)
			{
				result.AddFieldValidationResult(new FieldValidationResult(duplicate, ValidationResultType.Error, $"Inferred property name '{duplicate}' is occurs more than once. File: {csvFilePath}"));
			}

			return result;
		}
		#endregion
	}
}