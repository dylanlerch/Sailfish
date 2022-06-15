﻿using System.Reflection;
using System.Threading.Tasks;
using VeerPerforma.Attributes;
using VeerPerforma.Utils;

namespace VeerPerforma.Execution
{
    public class AncillaryInvocation
    {
        private readonly MethodInfo? globalSetup;
        private readonly MethodInfo? globalTeardown;
        private readonly object instance;
        private readonly MethodInfo? iterationSetup;
        private readonly MethodInfo? iterationTeardown;
        private readonly MethodInfo? methodSetup;
        private readonly MethodInfo? methodTeardown;
        private readonly PerformanceTimer performanceTimer;

        private readonly MethodInfo mainMethod;

        public AncillaryInvocation(object instance, MethodInfo method, PerformanceTimer performanceTimer)
        {
            this.instance = instance;
            mainMethod = method;
            this.performanceTimer = performanceTimer;
            globalSetup = instance.GetMethodWithAttribute<VeerGlobalSetupAttribute>();
            globalTeardown = instance.GetMethodWithAttribute<VeerGlobalTeardownAttribute>();
            methodSetup = instance.GetMethodWithAttribute<VeerExecutionMethodSetupAttribute>();
            methodTeardown = instance.GetMethodWithAttribute<VeerExecutionMethodTeardownAttribute>();
            iterationSetup = instance.GetMethodWithAttribute<VeerExecutionIterationSetupAttribute>();
            iterationTeardown = instance.GetMethodWithAttribute<VeerExecutionIterationTeardownAttribute>();
        }

        public async Task ExecutionMethod(bool timed = true)
        {
            if (timed) performanceTimer.StartExecutionTimer();
            await mainMethod.InvokeWith(instance);
            if (timed) performanceTimer.StopExecutionTimer();
        }

        public async Task IterationSetup()
        {
            if (iterationSetup is not null) await iterationSetup.InvokeWith(instance);
        }

        public async Task IterationTearDown()
        {
            if (iterationTeardown is not null) await iterationTeardown.InvokeWith(instance);
        }

        public async Task MethodSetup()
        {
            performanceTimer.StartMethodTimer();
            if (methodSetup is not null) await methodSetup.InvokeWith(instance);
        }

        public async Task MethodTearDown()
        {
            if (methodTeardown is not null) await methodTeardown.InvokeWith(instance);
            performanceTimer.StopMethodTimer();
        }


        public async Task GlobalSetup()
        {
            performanceTimer.StartGlobalTimer();
            if (globalSetup is not null) await globalSetup.InvokeWith(instance);
        }

        public async Task GlobalTeardown()
        {
            if (globalTeardown is not null) await globalTeardown.InvokeWith(instance);
            performanceTimer.StopGlobalTimer();
        }

        public PerformanceTimer GetPerformanceResults()
        {
            return performanceTimer;
        }
    }
}