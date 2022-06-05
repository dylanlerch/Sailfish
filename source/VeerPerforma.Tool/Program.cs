﻿using Autofac;
using McMaster.Extensions.CommandLineUtils;
using VeerPerforma.Executor;
using VeerPerforma.Tool.Framework.DIContainer;

namespace VeerPerforma.Tool;

class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    public void OnExecute()
    {
        if (TestNames is null) throw new Exception("Program failed to start...");
        ContainerConfiguration
            .CompositionRoot()
            .Resolve<VeerPerformaExecutor>()
            .Run(
                TestNames
                    .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                    .ToArray());
    }

    [Option("-t|--tests", CommandOptionType.MultipleValue, Description = "List of tests to execute")]
    public string[]? TestNames { get; set; } = new[] { "" };
}