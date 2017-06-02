// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    using System;
    using Catel;

    internal static class TypeExtensions
    {
        #region Methods
        public static T CreateInstanceOfType<T>(this Type type)
        {
            Argument.IsNotNull(() => type);
            Argument.IsOfType(nameof(type), type, typeof(T));

            return type != null ? (T) Activator.CreateInstance(type, new object[0]) : default(T);
        }
        #endregion
    }
}