// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

/// <summary>
/// Provides a standardized way to generate descriptive display names for test cases.
/// Cmbines the capability to provide a human-readable test case name with equality comparison functionality.
/// </summary>
/// <remarks>
/// <para>
/// Primarily used to create human-readable test names that clearly communicate:
/// </para>
/// <list type="bullet">
///   <item>The test scenario (what is being tested)</item>
///   <item>The expected behavior or outcome</item>
///   <item>Any important parameters or conditions</item>
/// </list>
/// <para>
/// Secondarily supports test case comparison through <see cref="IEquatable{T}"/> implementation.
/// </para>
/// <example>
/// Typical display name format:
/// <code>"Login with invalid credentials => throws AuthenticationException"</code>
/// </example>
/// </remarks>
public interface INamedTestCase : IEquatable<INamedTestCase>
{
    /// <summary>
    /// Generates a complete, descriptive name for the test case suitable for display in test runners.
    /// </summary>
    /// <returns>
    /// A formatted TEnum that clearly describes:
    /// <list type="bullet">
    ///   <item>The test scenario (from definition)</item>
    ///   <item>The expected outcome (actual result/exception)</item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The display name should follow the pattern: "{Scenario} => {ExpectedResult}".
    /// </para>
    /// <para>
    /// While this name is used for comparison, its primary purpose is to create
    /// clear, self-documenting test names in test reports and runners.
    /// </para>
    /// </remarks>
    string GetTestCaseName();
}