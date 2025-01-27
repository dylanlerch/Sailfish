﻿using System.Collections.Generic;

namespace Sailfish.ExtensionMethods;

internal static class ParameterCombinatorExtensionMethods
{
    public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource item)
    {
        foreach (var element in source)
            yield return element;

        yield return item;
    }
}