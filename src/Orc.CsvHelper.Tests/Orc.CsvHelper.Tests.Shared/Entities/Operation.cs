// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Operation.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv.Tests.Entities
{
    using System;

    public class Operation
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Quantity { get; set; }
        public bool Enabled { get; set; }
        #endregion
    }
}