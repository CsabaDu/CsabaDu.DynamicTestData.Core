// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.Statics;

/// <summary>
/// Specifies whether the test data object array contains the <see cref="ITestData"/> instance itself
/// or just its properties. This code determines how test data will be processed by the <see cref="IDataStrategy"/>.
/// </summary>
public enum ArgsCode
{
    /// <summary>
    /// Indicates that the test data object array contains the complete <see cref="ITestData"/> instance.
    /// When this code is used, the <see cref="PropsCode"/> values are ignored.
    /// </summary>
    Instance,

    /// <summary>
    /// Indicates that the test data object array contains only specific properties of the <see cref="ITestData"/> instance.
    /// Which properties are included is determined by the <see cref="PropsCode"/> value.
    /// </summary>
    Properties,
}