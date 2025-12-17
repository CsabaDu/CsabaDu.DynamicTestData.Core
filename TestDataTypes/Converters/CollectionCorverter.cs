// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Converters;

public static class CollectionCorverter
{
    public static IEnumerable<object?[]> Convert<TTestData>(
        this IEnumerable<TTestData> testDataCollection,
        ArgsCode argsCode,
        PropsCode propsCode)
    where TTestData : notnull, ITestData
    {
        ArgumentNullException.ThrowIfNull(
            testDataCollection,
            nameof(testDataCollection));

        foreach (var testData in testDataCollection)
        {
            yield return testData.ToParams(
                argsCode,
                propsCode);
        }
    }

    public static IEnumerable<TRow> Convert<TTestData, TRow>(
        this IEnumerable<TTestData> testDataCollection,
        Func<TTestData, ArgsCode, string?, TRow> testDataConverter,
        ArgsCode argsCode,
        string? testMethodName)
    where TTestData : notnull, ITestData
    {
        ArgumentNullException.ThrowIfNull(
            testDataCollection,
            nameof(testDataCollection));
        ArgumentNullException.ThrowIfNull(
            testDataConverter,
            nameof(testDataConverter));

        foreach (var testData in testDataCollection)
        {
            yield return testDataConverter(
                testData,
                argsCode,
                testMethodName);
        }
    }
}
