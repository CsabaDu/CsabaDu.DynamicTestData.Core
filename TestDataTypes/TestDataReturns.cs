// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

#region Abstract type
/// <summary>
/// Abstract base record for test data that expects a non-nullable <see cref="ValueType"/> return result.
/// </summary>
/// <typeparam name="TStruct">The value type of the expected return result (must be a non-nullable <see cref="ValueType").</typeparam>
/// <param name="Definition">Descriptive definition of the test scenario.</param>
/// <param name="Expected">The expected return value for the test case.</param>
/// <remarks>
/// Specializes <see cref="TestData"/> for test cases that verify return values of struct types.
/// </remarks>
public abstract record TestDataReturns<TStruct>(
    string Definition,
    TStruct Expected)
: TestData(Definition),
ITestDataReturns<TStruct>
where TStruct : struct
{
    /// <summary>
    /// Gets the formatted test case name including the expected return value.
    /// </summary>
    /// <example>
    /// "Test scenario => returns 42"
    /// </example>
    public string TestCaseName
    => GetTestCaseName($"returns {Expected.ToString() ?? nameof(Expected)}");

    /// <inheritdoc/>
    public override sealed string GetTestCaseName()
    => TestCaseName;

    /// <summary>
    /// Gets the expected return value as an object.
    /// </summary>
    /// <returns>The boxed expected value.</returns>
    public object GetExpected() => Expected;

    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)"/>
    /// <remarks>
    /// Adds the expected return value to the argument array when <see cref="ArgsCode.Properties"/> is specified.
    /// </remarks>
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Expected);
}
#endregion

#region Concrete types
/// <summary>
/// Test data implementation for return-value tests with 1 additional argument.
/// </summary>
/// <inheritdoc cref="TestDataReturns{TStruct}"/>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="Arg1">First test argument value.</param>
public record TestDataReturns<TStruct, T1>(
    string Definition,
    TStruct Expected,
    T1? Arg1)
: TestDataReturns<TStruct>(
    Definition,
    Expected),
    ITestData<TStruct, T1>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg1);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1}" />
/// <typeparam name="T2">The type of the second argument.</typeparam>
/// <param name="Arg2">The second argument.</param>
public record TestDataReturns<TStruct, T1, T2>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2)
: TestDataReturns<TStruct, T1>(
    Definition,
    Expected,
    Arg1),
    ITestData<TStruct, T1, T2>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg2);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2}" />
/// <typeparam name="T3">The type of the third argument.</typeparam>
/// <param name="Arg3">The third argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3)
: TestDataReturns<TStruct, T1, T2>(
    Definition,
    Expected,
    Arg1, Arg2),
    ITestData<TStruct, T1, T2, T3>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg3);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3}" />
/// <typeparam name="T4">The type of the fourth argument.</typeparam>
/// <param name="Arg4">The fourth argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
: TestDataReturns<TStruct, T1, T2, T3>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3),
    ITestData<TStruct, T1, T2, T3, T4>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg4);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3, T4}" />
/// <typeparam name="T5">The type of the fifth argument.</typeparam>
/// <param name="Arg5">The fifth argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4, T5>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
: TestDataReturns<TStruct, T1, T2, T3, T4>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4),
    ITestData<TStruct, T1, T2, T3, T4, T5>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg5);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3, T4, T5}" />
/// <typeparam name="T6">The type of the sixth argument.</typeparam>
/// <param name="Arg6">The sixth argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
: TestDataReturns<TStruct, T1, T2, T3, T4, T5>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5),
    ITestData<TStruct, T1, T2, T3, T4, T5, T6>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg6);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3, T4, T5, T6}" />
/// <typeparam name="T7">The type of the seventh argument.</typeparam>
/// <param name="Arg7">The seventh argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
: TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6),
    ITestData<TStruct, T1, T2, T3, T4, T5, T6, T7>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg7);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3, T4, T5, T6, T7}" />
/// <typeparam name="T8">The type of the eighth argument.</typeparam>
/// <param name="Arg8">The eighth argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7, T8>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8)
: TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7),
    ITestData<TStruct, T1, T2, T3, T4, T5, T6, T7, T8>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg8);
}

/// <inheritdoc cref="TestDataReturns{TStruct, T1, T2, T3, T4, T5, T6, T7, T8}" />
/// <typeparam name="T9">The type of the ninth argument.</typeparam>
/// <param name="Arg9">The ninth argument.</param>
public record TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    string Definition,
    TStruct Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8, T9? Arg9)
: TestDataReturns<TStruct, T1, T2, T3, T4, T5, T6, T7, T8>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8),
    ITestData<TStruct, T1, T2, T3, T4, T5, T6, T7, T8, T9>
where TStruct : struct
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg9);
}
#endregion
