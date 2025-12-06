// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;
/// <summary>
/// Marker interface for test cases validating method return values.
/// Inherits from <see cref="IExpected"/> and marks test data designed to return a value.
/// </summary>
/// <remarks>
/// Serves as a semantic indicator for tests verifying successful execution paths with return values.
/// </remarks>
public interface IReturns
: IExpected;

/// <summary>
/// Generic interface for test cases expecting specific non-nullable value type returns.
/// </summary>
/// <typeparam name="TStruct">The return value type (constrained to non-nullable value types).</typeparam>
/// <remarks>
/// Enables type-safe testing of methods returning:
/// <list type="bullet">
///   <item>Primitive values (int, bool, etc.)</item>
///   <item>Custom non-nullable value types</item>
///   <item>Other non-nullable structs</item>
/// </list>
/// </remarks>
public interface ITestDataReturns<out TStruct>
: IReturns,
ITestData<TStruct>
where TStruct : struct;
