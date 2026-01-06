// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

public abstract class TestData(
    string definition,
    string result)
: TestDataBase(definition)
{
    private readonly string _result = result;
    private const string ResultString = "result";

    public override sealed string GetResult()
    => GetOrSubstitute(_result, ResultString);

    public override sealed object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode)
    => Trim(base.ToParams, argsCode, propsCode,
        propsCode != PropsCode.TestCaseName);
}

#region Concrete types
/// <summary>
/// Test data implementation for test cases with 1 type argument.
/// </summary>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="definition">Description of the test scenario.</param>
/// <param name="result">The result expectedToString description.</param>
/// <param name="arg1">First test argument value.</param>
public class TestData<T1>(
    string definition,
    string result,
    T1? arg1)
: TestData(definition, result)
{
    public T1? Arg1 { get; init; } = arg1;

    /// <inheritdoc/>
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg1);
}

/// <summary>
/// Test data implementation for test cases with 2 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1}"/>
/// <typeparam name="T2">Type of the second test argument.</typeparam>
/// /// <param name="arg2">Second test argument value.</param>

public class TestData<T1, T2>(
    string definition,
    string result,
    T1? arg1, T2? arg2)
: TestData<T1>(definition, result, arg1)
{
    public T2? Arg2 { get; init; } = arg2;

    /// <inheritdoc/>
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg2);
}

/// <summary>
/// Test data implementation for test cases with 3 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2}"/>
/// <typeparam name="T3">Type of the third test argument.</typeparam>
/// /// <param name="arg3">Third test argument value.</param>
public class TestData<T1, T2, T3>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3)
: TestData<T1, T2>(definition, result, arg1, arg2)
{
    public T3? Arg3 { get; init; } = arg3;

    /// <inheritdoc/>
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg3);
}

/// <summary>
/// Test data implementation for test cases with 4 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3}" />
/// <typeparam name="T4">The type of the fourth argument.</typeparam>
/// <param name="arg4">The fourth test argument value..</param>
public class TestData<T1, T2, T3, T4>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4)
: TestData<T1, T2, T3>(
    definition,
    result,
    arg1, arg2, arg3)
{
    public T4? Arg4 { get; init; } = arg4;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg4);
}

/// <summary>
/// Test data implementation for test cases with 5 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4}" />
/// <typeparam name="T5">The type of the fifth argument.</typeparam>
/// <param name="arg5">The fifth test argument value..</param>
public class TestData<T1, T2, T3, T4, T5>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5)
: TestData<T1, T2, T3, T4>(
    definition,
    result,
    arg1, arg2, arg3, arg4)
{
    public T5? Arg5 { get; init; } = arg5;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg5);
}

/// <summary>
/// Test data implementation for test cases with 6 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5}" />
/// <typeparam name="T6">The type of the sixth argument.</typeparam>
/// <param name="arg6">The sixth test argument value..</param>
public class TestData<T1, T2, T3, T4, T5, T6>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6)
: TestData<T1, T2, T3, T4, T5>(
    definition,
    result,
    arg1, arg2, arg3, arg4, arg5)
{
    public T6? Arg6 { get; init; } = arg6;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg6);
}

/// <summary>
/// Test data implementation for test cases with 7 type arguments.
/// </summary>
// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6}" />
/// <typeparam name="T7">The type of the seventh argument.</typeparam>
/// <param name="arg7">The seventh test argument value..</param>
public class TestData<T1, T2, T3, T4, T5, T6, T7>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7)
: TestData<T1, T2, T3, T4, T5, T6>(
    definition,
    result,
    arg1, arg2, arg3, arg4, arg5, arg6)
{
    public T7? Arg7 { get; init; } = arg7;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg7);
}

/// <summary>
/// Test data implementation for test cases with 8 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6, T7}" />
/// <typeparam name="T8">The type of the eighth argument.</typeparam>
/// <param name="arg8">The eighth test argument value..</param>
public class TestData<T1, T2, T3, T4, T5, T6, T7, T8>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7, T8? arg8)
: TestData<T1, T2, T3, T4, T5, T6, T7>(
    definition,
    result,
    arg1, arg2, arg3, arg4, arg5, arg6, arg7)
{
    public T8? Arg8 { get; init; } = arg8;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg8);
}

/// <summary>
/// Test data implementation for test cases with 9 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6, T7, T8}" />
/// <typeparam name="T9">The type of the ninth argument.</typeparam>
/// <param name="arg9">The ninth test argument value..</param>
public class TestData<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    string definition,
    string result,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7, T8? arg8, T9? arg9)
: TestData<T1, T2, T3, T4, T5, T6, T7, T8>(
    definition,
    result,
    arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
{
    public T9? Arg9 { get; init; } = arg9;

    /// <inheritdoc cref="TestData.ToArgs(argsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg9);
}
#endregion
