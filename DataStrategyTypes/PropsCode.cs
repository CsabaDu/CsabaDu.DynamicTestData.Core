// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.DataStrategyTypes;

/// <summary>
/// Specifies which properties of an <see cref="ITestData"/> instance should be included in the test data object array
/// when <see cref="ArgsCode.Properties"/> is used. This works in conjunction with <see cref="IDataStrategy"/>.
/// </summary>
public enum PropsCode
{
    /// <summary>
    /// Includes all properties of the <see cref="TestDataTypes.Interfaces.ITestData"/> instance in the test data object array,
    /// including the 'TestCaseName'. This is the most comprehensive inclusion option.
    /// For MSTest: 'DynamicDataAttribute.DynamicDataDisplayName' can use
    /// 'DisplayNameFactory.CreateDisplayName' to construct the display name.
    /// </summary>
    TestCaseName,

    /// <summary>
    /// Includes all properties of the <see cref="TestDataTypes.Interfaces.ITestData"/> instance except the TestCaseName property.
    /// Most common case: Useful when the test case name isn't needed to be contained 
    /// by the test data object array.
    /// </summary>
    Expected,

    /// <summary>
    /// Excludes also the Expected property if the <see cref="TestDataTypes.Interfaces.ITestData"/> instance implements
    /// <see cref="TestDataTypes.Interfaces.IReturns"/>. Otherwise, the 'Expected' property is included.
    // Useful for NUnit/TestNG style tests returning values.
    /// </summary>
    Returns,

    /// <summary>
    /// Excludes the also Expected property if the <see cref="TestDataTypes.Interfaces.ITestData"/> instance implements
    /// <see cref="TestDataTypes.Interfaces.IThrows"/>. Otherwise, the Expected property is included.
    /// </summary>
    Throws,
}