using System;
using System.Collections.Generic;
using System.Reflection;

namespace VeerPerforma.Execution
{
    public interface ITestRunPreparation
    {
        Dictionary<string, List<(MethodInfo, object)>> GenerateTestInstances(Type test);
    }
}