// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Core;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Specializations;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Specializations.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

public abstract class TestDataThrows<TException>(
    string definition,
    TException expected)
: TestDataExpected<TException>(
    definition,
    expected),
IThrows<TException>
where TException : Exception
{
    private const string ThrowsString = "throws";

    /// <inheritdoc/>
    public override sealed string GetResultPrefix()
    => GetResultPrefix(ThrowsString);

    /// <summary>
    /// Gets the Expected exception instance.
    /// </summary>
    /// <returns>The exception object that should be thrown.</returns>
    public override sealed string GetResult()
    => GetExpectedResult(Expected.GetType().Name);

    public override sealed object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode)
    => Trim(base.ToParams, argsCode, propsCode,
        propsCode == PropsCode.Throws);
}

#region Concrete types
/// <summary>
/// Test data implementation for exception-throwing tests with 1 additional argument.
/// </summary>
/// <inheritdoc cref="TestDataThrows{TException}"/>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="arg1">The first argument value.</param>
public class TestDataThrows<TException, T1>(
    string definition,
    TException expected,
    T1? arg1)
: TestDataThrows<TException>(
    definition,
    expected)
where TException : Exception
{
    public T1? Arg1 { get; init; } = arg1;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg1);
}

/// <inheritdoc cref="TestDataThrows{TException, T1}" />
/// <typeparam name="T2">The type of the second argument.</typeparam>
/// <param name="arg2">The second argument.</param>
public class TestDataThrows<TException, T1, T2>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2)
: TestDataThrows<TException, T1>(
    definition,
    expected,
    arg1)
where TException : Exception
{
    public T2? Arg2 { get; init; } = arg2;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg2);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2}" />
/// <typeparam name="T3">The type of the third argument.</typeparam>
/// <param name="arg3">The third argument.</param>
public class TestDataThrows<TException, T1, T2, T3>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3)
: TestDataThrows<TException, T1, T2>(
    definition,
    expected,
    arg1, arg2)
where TException : Exception
{
    public T3? Arg3 { get; init; } = arg3;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg3);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3}" />
/// <typeparam name="T4">The type of the fourth argument.</typeparam>
/// <param name="arg4">The fourth argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4)
: TestDataThrows<TException, T1, T2, T3>(
    definition,
    expected,
    arg1, arg2, arg3)
where TException : Exception
{
    public T4? Arg4 { get; init; } = arg4;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg4);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4}" />
/// <typeparam name="T5">The type of the fifth argument.</typeparam>
/// <param name="arg5">The fifth argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4, T5>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5)
: TestDataThrows<TException, T1, T2, T3, T4>(
    definition,
    expected,
    arg1, arg2, arg3, arg4)
where TException : Exception
{
    public T5? Arg5 { get; init; } = arg5;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg5);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5}" />
/// <typeparam name="T6">The type of the sixth argument.</typeparam>
/// <param name="arg6">The sixth argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6)
: TestDataThrows<TException, T1, T2, T3, T4, T5>(
    definition,
    expected,
    arg1, arg2, arg3, arg4, arg5)
where TException : Exception
{
    public T6? Arg6 { get; init; } = arg6;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg6);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6}" />
/// <typeparam name="T7">The type of the seventh argument.</typeparam>
/// <param name="arg7">The seventh argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(
    definition,
    expected,
    arg1, arg2, arg3, arg4, arg5, arg6)
where TException : Exception
{
    public T7? Arg7 { get; init; } = arg7;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg7);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6, T7}" />
/// <typeparam name="T8">The type of the eighth argument.</typeparam>
/// <param name="arg8">The eighth argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7, T8? arg8)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7>(
    definition,
    expected,
    arg1, arg2, arg3, arg4, arg5, arg6, arg7)
where TException : Exception
{
    public T8? Arg8 { get; init; } = arg8;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg8);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6, T7, T8}" />
/// <typeparam name="T9">The type of the ninth argument.</typeparam>
/// <param name="arg9">The ninth argument.</param>
public class TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    string definition,
    TException expected,
    T1? arg1, T2? arg2, T3? arg3, T4? arg4, T5? arg5, T6? arg6, T7? arg7, T8? arg8, T9? arg9)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8>(
    definition,
    expected,
    arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
where TException : Exception
{
    public T9? Arg9 { get; init; } = arg9;

    /// <inheritdoc cref="TestDataBase.ToArgs(ArgsCode)" />
    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Arg9);
}
#endregion
