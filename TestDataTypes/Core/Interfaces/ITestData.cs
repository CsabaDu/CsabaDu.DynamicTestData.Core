// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Foundational.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Core.Interfaces;

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
    string GetDefinition();

    /// <summary>
    /// Returns the Expected value of the test case.
    /// </summary>
    string GetResult();

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
