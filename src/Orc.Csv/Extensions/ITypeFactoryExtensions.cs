// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeFactoryExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using Catel;
    using Catel.IoC;
    using CsvHelper.Configuration;

    internal static class ITypeFactoryExtensions
    {
        #region Methods
        public static ClassMap TryToCreateClassMap(this ITypeFactory typeFactory, Type type)
        {
            Argument.IsNotNull(() => typeFactory);

            return type is not null ? typeFactory.CreateInstanceWithParametersAndAutoCompletion(type) as ClassMap : null;
        }
        #endregion
    }
}