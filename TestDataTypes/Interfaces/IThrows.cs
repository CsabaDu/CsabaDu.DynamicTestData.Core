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
public interface IThrows
: IExpected;

public interface IThrows<out TException>
: IExpected<TException>,
IThrows
where TException : Exception;