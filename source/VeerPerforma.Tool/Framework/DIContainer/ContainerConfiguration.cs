﻿using Autofac;
using VeerPerforma.Executor;

namespace VeerPerforma.Tool.Framework.DIContainer;

public static class ContainerConfiguration
{
    public static IContainer CompositionRoot()
    {
        var builder = new ContainerBuilder();
        builder = CustomizeContainer(builder);
        builder.RegisterType<VeerPerformaExecutor>();
        return builder.Build();
    }

    private static ContainerBuilder CustomizeContainer(ContainerBuilder builder)
    {
        return builder;
    }
}