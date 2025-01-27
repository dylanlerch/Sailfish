﻿using System;
using System.Linq;

namespace Sailfish.Execution;

internal class TestFilter : ITestFilter
{
    private readonly ITestListValidator testListValidator;

    public TestFilter(ITestListValidator testListValidator)
    {
        this.testListValidator = testListValidator;
    }

    public TestValidationResult FilterAndValidate(Type[] tests, string[] testsRequestedByUser)
    {
        var filtered = FilterTests(tests, testsRequestedByUser);
        var result = testListValidator.ValidateTests(testsRequestedByUser, filtered);
        return result;
    }

    private Type[] FilterTests(Type[] tests, string[] testsRequestedByUser)
    {
        if (testsRequestedByUser.Length > 0)
            return tests
                .Where(test => testsRequestedByUser.Contains(test.Name))
                .ToArray();

        return tests;
    }
}