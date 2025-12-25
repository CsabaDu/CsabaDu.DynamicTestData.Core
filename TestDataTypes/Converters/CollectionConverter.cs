// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;
using CsabaDu.DynamicTestData.Core.Validators;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Converters;

/// <summary>
/// Provides extension methods for transforming collections of <see cref="ITestData"/> 
/// into framework-ready representations. Acts as a dual-strategy converter supporting 
/// both parameter arrays and custom row formats.
/// </summary>
public static class CollectionConverter
{
    /// <summary>
    /// Converts a collection of <see cref="ITestData"/> into an enumeration of 
    /// parameter arrays (<c>object?[]</c>) suitable for direct consumption by 
    /// parameterized test runners (e.g., NUnit, xUnit, MSTest).
    /// </summary>
    /// <typeparam name="TTestData">
    /// The concrete test data type, constrained to <see cref="ITestData"/>.
    /// </typeparam>
    /// <param name="testDataCollection">
    /// The source collection of test data objects to be transformed.
    /// </param>
    /// <param name="argsCode">
    /// Specifies the argument transformation strategy (e.g., instance vs. properties).
    /// </param>
    /// <param name="propsCode">
    /// Specifies the property transformation semantics (e.g., Expected, returns, throws).
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of parameter arrays, each representing a 
    /// fully shaped test case for direct invocation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="testDataCollection"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<object?[]> Convert<TTestData>(
        this IEnumerable<TTestData> testDataCollection,
        ArgsCode argsCode,
        PropsCode propsCode)
    where TTestData : notnull, ITestData
    => testDataCollection.ConvertDistinct(
        testData => testData.ToParams(argsCode, propsCode));

    /// <summary>
    /// Converts a collection of <see cref="ITestData"/> into an enumeration of
    /// parameter arrays using the default <see cref="PropsCode.Expected"/> semantics.
    /// </summary>
    /// <typeparam name="TTestData">
    /// The concrete test data type, constrained to <see cref="ITestData"/>.
    /// </typeparam>
    /// <param name="testDataCollection">
    /// The source collection of test data objects to convert.
    /// </param>
    /// <param name="argsCode">
    /// Specifies the argument transformation strategy (e.g., instance vs. properties).
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of parameter arrays representing the
    /// transformed test cases.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="testDataCollection"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<object?[]> Convert<TTestData>(
        this IEnumerable<TTestData> testDataCollection,
        ArgsCode argsCode)
    where TTestData : notnull, ITestData
    => testDataCollection.ConvertDistinct(
        testData => testData.ToParams(argsCode));

    /// <summary>
    /// Converts a collection of <see cref="ITestData"/> into a sequence of custom 
    /// row objects using a caller-provided transformation function. This enables 
    /// integration with external systems such as reporting tools, visualizers, 
    /// or databases.
    /// </summary>
    /// <typeparam name="TTestData">
    /// The concrete test data type, constrained to <see cref="ITestData"/>.
    /// </typeparam>
    /// <typeparam name="TRow">
    /// The target row type produced by the transformation function.
    /// </typeparam>
    /// <param name="testDataCollection">
    /// The source collection of test data objects to be transformed.
    /// </param>
    /// <param name="testDataConverter">
    /// A delegate that defines how each test data object is converted into a 
    /// <typeparamref name="TRow"/> instance.
    /// </param>
    /// <param name="argsCode">
    /// Specifies the argument transformation strategy (e.g., instance vs. properties).
    /// </param>
    /// <param name="testMethodName">
    /// An optional test method name used for constructing descriptive display names.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of custom row objects, each representing 
    /// a transformed test case suitable for external consumption.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="testDataCollection"/> or <paramref name="testDataConverter"/> is <c>null</c>.
    /// </exception>
    public static IEnumerable<TRow> Convert<TTestData, TRow>(
        this IEnumerable<TTestData> testDataCollection,
        Func<TTestData, ArgsCode, string?, TRow> testDataConverter,
        ArgsCode argsCode,
        string? testMethodName)
    where TTestData : notnull, ITestData
    => testDataCollection.ConvertDistinct(
        testData => testDataConverter(
            testData,
            argsCode.Defined(nameof(argsCode)),
            testMethodName));

    /// <summary>
    /// Core transformation routine used by all public <c>CollectionConverter</c>
    /// methods to convert a sequence of <see cref="ITestData"/> items into
    /// custom row objects.
    /// </summary>
    /// <typeparam name="TTestData">
    /// The concrete test data type, constrained to <see cref="ITestData"/>.
    /// </typeparam>
    /// <typeparam name="TRow">
    /// The target row type produced by the <paramref name="testDataConverter"/> delegate.
    /// </typeparam>
    /// <param name="testDataCollection">
    /// The source collection of test data items to be transformed.
    /// </param>
    /// <param name="testDataConverter">
    /// A delegate that converts a single <typeparamref name="TTestData"/> instance
    /// into a <typeparamref name="TRow"/> result.
    /// </param>
    /// <returns>
    /// A lazily streamed <see cref="IEnumerable{T}"/> of semantically distinct
    /// row objects, each representing a unique test case.
    /// </returns>
    /// <remarks>
    /// This private method provides the shared transformation pipeline for all
    /// public <c>CollectionConverter</c> overloads. It performs semantic
    /// deduplication using <see cref="INamedTestCase"/> identity and equality
    /// semantics, ensuring that each logical test case is emitted only once.
    /// Results are streamed using <c>yield return</c> for efficient, on‑demand
    /// processing.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="testDataCollection"/> or
    /// <paramref name="testDataConverter"/> is <c>null</c>.
    /// </exception>
    private static IEnumerable<TRow> ConvertDistinct<TTestData, TRow>(
        this IEnumerable<TTestData> testDataCollection,
        Func<TTestData, TRow> testDataConverter)
    where TTestData : notnull, ITestData
    {
        ArgumentNullException.ThrowIfNull(
            testDataCollection,
            nameof(testDataCollection));
        ArgumentNullException.ThrowIfNull(
            testDataConverter,
            nameof(testDataConverter));

        // Deduplicate based on INamedTestCase identity/equality semantics
        HashSet<INamedTestCase> testCases = new(NamedTestCase.Comparer);

        foreach (var testData in testDataCollection)
        {
            if (testCases.Add(testData))
            {
                yield return testDataConverter(testData);
            }
        }
    }
}
