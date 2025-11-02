// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

/// <summary>
/// Marker interface for test cases validating exception throwing behavior.
/// Inherits from <see cref="IExpected"/> and marks test data designed to throw an exception.
/// </summary>
/// <remarks>
/// Identifies tests verifying error handling and exceptional execution paths.
/// </remarks>
public interface ITestDataThrows
: IExpected;

/// <summary>
/// Generic interface for test cases expecting specific exception types.
/// </summary>
/// <typeparam name="TException">The expected exception type (derived from <see cref="Exception"/>).</typeparam>
/// <remarks>
/// Facilitates type-safe exception validation for:
/// <list type="bullet">
///   <item>Expected error conditions</item>
///   <item>Exception type verification</item>
///   <item>Failure scenario testing</item>
/// </list>
/// </remarks>
public interface ITestDataThrows<out TException>
: ITestDataThrows,
ITestData<TException>
where TException : Exception;
