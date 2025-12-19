// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Converters;

/// <summary>
/// Provides extension methods for transforming collections of <see cref="ITestData"/> 
/// into framework-ready representations. Acts as a dual-strategy converter supporting 
/// both parameter arrays and custom row formats.
/// </summary>
public static class CollectionConverter
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
