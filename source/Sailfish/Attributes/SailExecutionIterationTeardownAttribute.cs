using System;

namespace Sailfish.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SailExecutionIterationTeardownAttribute : Attribute
    {
    }
}