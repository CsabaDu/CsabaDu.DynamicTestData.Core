// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

/// <summary>
/// Core interface representing test data with basic test case functionality.
/// </summary>
/// <remarks>
/// Provides fundamental operations for:
/// <list type="bullet">
///   <item>Test case naming and identification (via <see cref="INamedTestCase"/>)</item>
///   <item>Test scenario definition</item>
///   <item>Argument generation for test execution</item>
/// </list>
/// </remarks>
public interface ITestData : INamedTestCase
{
    /// <summary>
    /// Gets the description of the test scenario being verified.
    /// </summary>
    string Definition { get; }

    ///// <summary>
    ///// Converts the test case to an array of arguments based on the specified <see cref="ArgsCode"> parameter.
    ///// </summary>
    ///// <param name="argsCode">Determines whether to include the test data instance or just its properties.</param>
    ///// <returns>
    ///// An array of arguments ready for test execution.
    ///// </returns>
    //object?[] ToArgs(ArgsCode argsCode);

    object?[] ToParams(ArgsCode argsCode);

    /// <summary>
    /// Converts the test case to parameters with precise control over included elements.
    /// </summary>
    /// <param name="argsCode">Determines instance vs properties inclusion.</param>
    /// <param name="propsCode">Specifies which properties to include.</param>
    /// <returns>
    /// A parameter array tailored for test execution.
    /// </returns>
    object?[] ToParams(ArgsCode argsCode, PropsCode propsCode);
}

/// <summary>
/// Represents a generic test data interface that extends <see cref="ITestData" with the generic type of the Expected non-nullable result of the test case.
/// </summary>
/// <typeparam name="TResult">The type of Expected result (non-nullable).</typeparam>
/// <remarks>
/// Extends <see cref="ITestData"/> with type-safe Expected result handling.
/// </remarks>
public interface ITestData<out TResult> : ITestData
    where TResult : notnull
{
    /// <summary>
    /// Gets the Expected result of the test case.
    /// </summary>
    TResult Expected { get; }
}
