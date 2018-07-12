// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicApiFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests
{
    using System.Runtime.CompilerServices;
    using ApiApprover;
    using Csv;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_CsvHelper_HasNoBreakingChanges()
        {
            var assembly = typeof(CsvReaderService).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}