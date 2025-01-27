﻿using System;
using System.Linq;
using System.Reflection;

namespace Sailfish.ExtensionMethods;

internal static class TypeExtensionMethods
{
    public static ConstructorInfo GetSingleConstructor(this Type type)
    {
        var ctorInfos = type
            .GetConstructors(
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.DeclaredOnly
                | BindingFlags.NonPublic);
        if (ctorInfos.Length == 0 || ctorInfos.Length > 1) throw new Exception("A single ctor must be declared in all test types");
        return ctorInfos.Single();
    }

    public static Type[] GetCtorParamTypes(this Type type)
    {
        var ctorInfo = type.GetSingleConstructor();
        var argTypes = ctorInfo.GetParameters().Select(x => x.ParameterType).ToArray();
        return argTypes;
    }
}