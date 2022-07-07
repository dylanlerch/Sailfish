﻿using System;
using Sailfish.Attributes;

namespace AsAConsoleApp.ExamplePerformanceTests;

[Sailfish(disabled: true)]
public class FaultyTestStructure
{
    [SailfishVariable(1, 2, 3)]
    public int Variable { get; set; }

    [SailfishMethod]
    public void ILackASailfishMethodAttribute()
    {
        Console.WriteLine("TEST");
    }
}