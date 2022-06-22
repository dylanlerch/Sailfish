﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace VeerPerforma.Execution
{
    public class TestCaseIterator : ITestCaseIterator
    {
        private readonly ILogger logger;

        public TestCaseIterator(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<List<string>> Iterate(TestInstanceContainer testInstanceContainer)
        {
            await testInstanceContainer.Invocation.GlobalSetup();

            await WarmupIterations(testInstanceContainer);

            var messages = new List<string>();
            for (var i = 0; i < testInstanceContainer.NumIterations; i++)
            {
                await testInstanceContainer.Invocation.IterationSetup();

                await testInstanceContainer.Invocation.ExecutionMethod();

                await testInstanceContainer.Invocation.IterationTearDown();
            }

            await testInstanceContainer.Invocation.GlobalTeardown();

            return messages; // TODO: use this?
        }

        private async Task WarmupIterations(TestInstanceContainer testInstanceContainer)
        {
            for (var i = 0; i < testInstanceContainer.NumWarmupIterations; i++)
            {
                await testInstanceContainer.Invocation.IterationSetup();
                await testInstanceContainer.Invocation.ExecutionMethod();
                await testInstanceContainer.Invocation.IterationTearDown();
            }
        }
    }
}