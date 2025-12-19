// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Factories;

public static class DisplayNameFactory
{
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
