namespace Orc.Csv.Tests.Entities;

using System;
using System.Collections.Generic;

public class Operation
{
    public Operation()
    {
        Attributes  = new Dictionary<string, string>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public double Quantity { get; set; }
    public bool Enabled { get; set; }

    public Dictionary<string, string> Attributes { get; }

    public Operation Clone()
    {
        return new Operation
        {
            Id = Id,
            Name = Name,
            StartTime = StartTime,
            Duration = Duration,
            Quantity = Quantity,
            Enabled = Enabled,
        };
    }
}
