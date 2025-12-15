// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Factories;

public static class ParamsFactory
{
    public static IEnumerable<object?[]>? CreateParams<TTestData>(
        IEnumerable<TTestData> testDataSource,
        ArgsCode argsCode,
        PropsCode propsCode)
    where TTestData : notnull, ITestData
    {
        foreach (var testData in testDataSource)
        {
            yield return testData.ToParams(argsCode, propsCode);
        }
    }
}