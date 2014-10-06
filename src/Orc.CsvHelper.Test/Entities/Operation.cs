namespace Orc.Csv.Test.Entities
{
    using System;

    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Quantity { get; set; }
        public bool Enabled { get; set; }
    }
}