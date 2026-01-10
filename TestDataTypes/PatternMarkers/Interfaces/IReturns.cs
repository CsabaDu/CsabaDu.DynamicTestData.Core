// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.PatternMarkers.Interfaces;

/// <summary>
/// Marker interface for test cases validating method return values.
/// Inherits from <see cref="IExpected"/> and marks test data designed to return a value.
/// </summary>
/// <remarks>
/// Serves as a semantic indicator for tests verifying successful execution paths with return values.
/// </remarks>
public interface IReturns
: IExpected;

public interface IReturns<out TResult>
: IExpected<TResult>,
IReturns
where TResult : notnull;