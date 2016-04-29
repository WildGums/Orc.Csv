// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeGenerationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    public interface ICodeGenerationService
    {
        #region Methods
        void CreateCSharpFilesForAllCsvFiles(string inputFoler, string namespaceName, string outputFolder);
        string[] GetCsvFiles(string folderPath);
        void CreateCSharpFiles(string csvFilePath, string namespaceName, string outputFolder);
        #endregion
    }
}