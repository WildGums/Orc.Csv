// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICsvValidationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
	using System.Collections.Generic;
	using Catel.Data;

	public interface ICsvValidationService
	{
		IValidationContext Validate(string csvFilePath);
		IValidationContext Validate(string csvFilePath, string className, IEnumerable<string> propertyNames);
	}
}