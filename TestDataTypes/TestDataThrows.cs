// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

#region Abstract type
/// <summary>
/// Abstract base record for test data that expects exception throwing behavior.
/// </summary>
/// <typeparam name="TException">The type of exception expected to be thrown (must derive from <see cref="Exception"/>).</typeparam>
/// <param name="Definition">Description of the test scenario that should throw.</param>
/// <param name="Expected">The exception instance expected to be thrown.</param>
/// <remarks>
/// Specializes <see cref="TestData"/> for test cases that verify exception throwing behavior.
/// Provides consistent test case naming through the base <see cref="TestData.GetTestCaseName(string)"/> method.
/// </remarks>
public abstract record TestDataThrows<TException>(
    string Definition,
    TException Expected)
: TestData(Definition),
ITestDataThrows<TException>
where TException : Exception
{
    /// <summary>
    /// Gets the formatted test case name including the expected exception runtime type name.
    /// </summary>
    /// <example>
    /// "Invalid login => throws ArgumentException"
    /// </example>
    public string TestCaseName
    => GetTestCaseName($"throws {Expected.GetType().Name}");

    /// <inheritdoc/>
    public override sealed string GetTestCaseName()
    => TestCaseName;

    /// <summary>
    /// Gets the expected exception instance.
    /// </summary>
    /// <returns>The exception object that should be thrown.</returns>
    public object GetExpected() => Expected;

    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)"/>
    /// <remarks>
    /// Adds the expected exception to the argument array when <see cref="ArgsCode.Properties"/> is specified.
    /// </remarks>
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Expected);
}
#endregion

#region Concrete types
/// <summary>
/// Test data implementation for exception-throwing tests with 1 additional argument.
/// </summary>
/// <inheritdoc cref="TestDataThrows{TException}"/>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="Arg1">The first argument value.</param>
public record TestDataThrows<TException, T1>(
    string Definition,
    TException Expected,
    T1? Arg1)
: TestDataThrows<TException>(
    Definition,
    Expected),
    ITestData<TException, T1>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg1);
}

/// <inheritdoc cref="TestDataThrows{TException, T1}" />
/// <typeparam name="T2">The type of the second argument.</typeparam>
/// <param name="Arg2">The second argument.</param>
public record TestDataThrows<TException, T1, T2>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2)
: TestDataThrows<TException, T1>(
    Definition,
    Expected,
    Arg1),
    ITestData<TException, T1, T2>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg2);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2}" />
/// <typeparam name="T3">The type of the third argument.</typeparam>
/// <param name="Arg3">The third argument.</param>
public record TestDataThrows<TException, T1, T2, T3>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3)
: TestDataThrows<TException, T1, T2>(
    Definition,
    Expected,
    Arg1, Arg2),
    ITestData<TException, T1, T2, T3>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg3);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3}" />
/// <typeparam name="T4">The type of the fourth argument.</typeparam>
/// <param name="Arg4">The fourth argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
: TestDataThrows<TException, T1, T2, T3>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3),
    ITestData<TException, T1, T2, T3, T4>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg4);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4}" />
/// <typeparam name="T5">The type of the fifth argument.</typeparam>
/// <param name="Arg5">The fifth argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4, T5>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
: TestDataThrows<TException, T1, T2, T3, T4>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4),
    ITestData<TException, T1, T2, T3, T4, T5>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg5);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5}" />
/// <typeparam name="T6">The type of the sixth argument.</typeparam>
/// <param name="Arg6">The sixth argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
: TestDataThrows<TException, T1, T2, T3, T4, T5>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5),
    ITestData<TException, T1, T2, T3, T4, T5, T6>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg6);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6}" />
/// <typeparam name="T7">The type of the seventh argument.</typeparam>
/// <param name="Arg7">The seventh argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6),
    ITestData<TException, T1, T2, T3, T4, T5, T6, T7>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg7);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6, T7}" />
/// <typeparam name="T8">The type of the eighth argument.</typeparam>
/// <param name="Arg8">The eighth argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7),
    ITestData<TException, T1, T2, T3, T4, T5, T6, T7, T8>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg8);
}

/// <inheritdoc cref="TestDataThrows{TException, T1, T2, T3, T4, T5, T6, T7, T8}" />
/// <typeparam name="T9">The type of the ninth argument.</typeparam>
/// <param name="Arg9">The ninth argument.</param>
public record TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    string Definition,
    TException Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8, T9? Arg9)
: TestDataThrows<TException, T1, T2, T3, T4, T5, T6, T7, T8>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8),
    ITestData<TException, T1, T2, T3, T4, T5, T6, T7, T8, T9>
where TException : Exception
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg9);
}
#endregion
