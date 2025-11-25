// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.Statics;

/// <summary>
/// Specifies which properties of an <see cref="ITestData"/> instance should be included in the test data object array
/// when <see cref="ArgsCode.Properties"/> is used. This works in conjunction with <see cref="IDataStrategy"/>.
/// </summary>
public enum PropsCode
{
    /// <summary>
    /// Includes all properties of the <see cref="ITestData"/> instance in the test data object array,
    /// including the TestCaseName property. This is the most comprehensive inclusion option.
    /// </summary>
    TestCaseName,

    /// <summary>
    /// Includes all properties of the <see cref="ITestData"/> instance except the TestCaseName property.
    /// This is useful when the test case name isn't needed to be contained by the test data object array.
    /// </summary>
    Expected,

    /// <summary>
    /// Excludes the Expected property if the <see cref="ITestData"/> instance implements
    /// <see cref="ITestDataReturns"/>. Otherwise, the Expected property is included.
    /// </summary>
    Returns,

    /// <summary>
    /// Excludes the Expected property if the <see cref="ITestData"/> instance implements
    /// <see cref="ITestDataThrows"/>. Otherwise, the Expected property is included.
    /// </summary>
    Throws,
}