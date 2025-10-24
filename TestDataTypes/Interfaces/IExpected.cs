// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.TestDataTypes.Interfaces;

/// <summary>
/// Represents a base interface for test data that has a primary test parameter for test case result.
/// </summary>
public interface IExpected
{
    /// <summary>
    /// Returns the expected value of the test case.
    /// </summary>
    object GetExpected();
}
