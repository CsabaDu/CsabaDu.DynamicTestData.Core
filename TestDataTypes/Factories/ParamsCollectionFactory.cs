// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Factories;

public static class ParamsCollectionFactory
{
    public static IEnumerable<object?[]> CreateParamsCollection(
        IEnumerable<ITestData> testDataSource,
        ArgsCode argsCode,
        PropsCode propsCode)
    {
        foreach (var testData in testDataSource)
        {
            yield return testData.ToParams(
                argsCode,
                propsCode);
        }
    }

    public static IEnumerable<T> CreateParamsCollection<TTestData, T>(
        IEnumerable<TTestData> testDataSource,
        Func<TTestData, ArgsCode, PropsCode, string?, T> convert,
        ArgsCode argsCode,
        PropsCode propsCode,
        string? testMethodName)
    where TTestData : notnull, ITestData
    {
        foreach (var testData in testDataSource)
        {
            yield return convert(
                testData,
                argsCode,
                propsCode,
                testMethodName);
        }
    }
}