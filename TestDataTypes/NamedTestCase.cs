// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

public abstract class NamedTestCase : INamedTestCase
{
    /// <summary>
    /// Gets the unique name identifying this test case.
    /// </summary>
    public abstract string TestCaseName { get; }

    /// <summary>
    /// Determines whether the current instance is contained within the specified collection of named test cases.
    /// </summary>
    /// <param name="namedTestCases">The collection of <see cref="INamedTestCase"/> instances to search. Can be <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the current instance is found in the specified collection; otherwise, <see
    /// langword="false"/>.  Returns <see langword="false"/> if <paramref name="namedTestCases"/> is <see
    /// langword="null"/>.</returns>
    public bool ContainedBy(IEnumerable<INamedTestCase>? namedTestCases)
    => namedTestCases?.Any(Equals) == true;

    public string? GetDisplayName(string? testMethodName)
    => CreateDisplayName(testMethodName, TestCaseName);

    /// <summary>
    /// Determines equality with another <see cref="INamedTestCase"/> based on test case name comparison.
    /// </summary>
    /// <param name="other">The <see cref="INamedTestCase"/> to compare against.</param>
    /// <returns>
    /// <c>true</c> if the test case names match; otherwise <c>false</c>.
    /// </returns>
    public bool Equals(INamedTestCase? other)
    => other?.TestCaseName == TestCaseName;

    public override sealed bool Equals(object? obj)
    => Equals(obj as INamedTestCase);

    /// <summary>
    /// Generates a hash code derived from the return value of the <see cref="GetTestCaseName"/> method.
    /// </summary>
    /// <returns>A stable hash code for the test case.</returns>
    public override sealed int GetHashCode()
    => TestCaseName.GetHashCode();

    /// <summary>
    /// Overrides and seals the `ToString()` method to return the value of <see cref="GetTestCaseName"/> method.
    /// </summary>
    public override sealed string ToString()
    => TestCaseName;

    /// <summary>
    /// Generates a display name for test cases combining method name and test data.
    /// </summary>
    /// <param name="testMethodName">Name of the test method.</param>
    /// <param name="args">Test arguments (first argument should be the test case name).</param>
    /// <returns>
    /// Formatted TEnum in pattern: "{testMethodName}(testData: {testCaseName})",
    /// or null if inputs are invalid.
    /// </returns>
    /// <example>
    /// <code>
    /// CreateDisplayName("LoginTest", testData) // "LoginTest(testData: Invalid login)"
    /// </code>
    /// </example>
    public static string? CreateDisplayName(string? testMethodName, params object?[]? args)
    {
        if (string.IsNullOrEmpty(testMethodName)) return null;

        var testCaseName = args?.FirstOrDefault();
        var argToString = testCaseName?.ToString();

        if (string.IsNullOrEmpty(argToString)) return null;

        return $"{testMethodName}(testData: {testCaseName})";
    }
}
