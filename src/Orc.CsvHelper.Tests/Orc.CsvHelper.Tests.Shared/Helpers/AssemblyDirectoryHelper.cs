// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyDirectoryHelper.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.CsvHelper.Tests
{
    using System;
    using Catel.IO;

    internal static class AssemblyDirectoryHelper
    {
        #region Constants
        private static string _testDataDirectory;
        private static string _currentDirectory;
        #endregion

        #region Methods
        public static string GetCurrentDirectory()
        {
            return _currentDirectory ?? (_currentDirectory = AppDomain.CurrentDomain.BaseDirectory);
        }

        public static string GetTestDataDirectory()
        {
            return _testDataDirectory ?? (_testDataDirectory = Path.Combine(AssemblyDirectoryHelper.GetCurrentDirectory(), "TestData"));
        }
        #endregion
    }
}