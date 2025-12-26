// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;
using CsabaDu.DynamicTestData.Core.Validators;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

#region Abstract type
/// <summary>
/// Abstract base class representing test case data with core functionality for test argument generation.
/// </summary>
/// <param name="definition">The descriptive definition of the test case scenario.</param>
/// <remarks>
/// Provides foundational implementation for:
/// <list type="bullet">
/// <item>Test case naming and identification</item>
/// <item>argument generation strategies</item>
/// <item>Equality comparison based on test case names</item>
/// <item>Conversion to parameter arrays for test execution</item>
/// </list>
/// </remarks>
public abstract class TestData(string definition)
: NamedTestCase, ITestData
{
    #region Fields
    private readonly string _definition = definition;
    #endregion

    #region Properties
    public override sealed string TestCaseName
    {
        get
        {
            var definition = GetDefinition();
            var result = GetResult();

            return $"{definition} => {result}";
        }
    }
    #endregion

    #region Methods
    public string GetDefinition()
    => GetOrSubstitute(_definition, "definition");

    public object?[] ToParams(ArgsCode argsCode)
    => ToParams(argsCode, PropsCode.Expected);

    /// <summary>
    /// Converts the test data to a parameter array with precise control over included properties.
    /// </summary>
    /// <param name="ArgsCode">Determines instance vs properties inclusion.</param>
    /// <param name="propsCode">Specifies which properties to include when using <see cref="ArgsCode.Properties"/>.</param>
    /// <returns>
    /// A parameter array tailored for test execution based on the specified codes.
    /// </returns>
    /// <exception cref="InvalidEnumargumentException">
    /// Thrown when invalid enum values are provided.
    /// </exception>
    public virtual object?[] ToParams(ArgsCode argsCode, PropsCode propsCode)
    => ToArgs(argsCode);

        //return argsCode switch
        //{
        //    ArgsCode.Instance => baseArgs,
        //    ArgsCode.Properties => propsCode switch
        //    {
        //        PropsCode.TestCaseName => baseArgs,
        //        PropsCode.Expected => argsWithoutTestCaseName(),
        //        PropsCode.Returns => argsWithoutExpectedIf(this is IReturns),
        //        PropsCode.Throws => argsWithoutExpectedIf(this is IThrows),
        //        _ => throw propsCode.GetInvalidEnumArgumentException(nameof(propsCode)),
        //    },
        //    _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
        //};

        //#region Local methods
        //object?[] argsWithoutExpectedIf(bool typeMatches)
        //=> typeMatches ?
        //    argsFrom(idxExpected + 1)
        //    : argsWithoutTestCaseName();

        //object?[] argsWithoutTestCaseName()
        //=> argsFrom(idxExpected);

        //object?[] argsFrom(int index)
        //=> baseArgs.Length > index ?
        //    baseArgs[index..]
        //    : throw new ArgumentOutOfRangeException(
        //        nameof(propsCode),
        //        $"Insufficient 'PropsCode' for the requested operation. " +
        //        $"baseArgs.Length={baseArgs.Length}, requiredIndex={index}, testCase={TestCaseName}");
        //#endregion
    //}

    public abstract string GetResult();
    #endregion

    #region Helper methods

    protected static string GetOrSubstitute(string? value, string substitute)
    => string.IsNullOrEmpty(value) ?
        substitute
        : value;

    /// <summary>
    /// Converts the test data to an argument array based on the specified <see cref="ArgsCode"/> parameter.
    /// </summary>
    /// <param name="ArgsCode">Determines whether to include the instance itself or its properties.</param>
    /// <returns>
    /// An array containing:
    /// <list type="bullet">
    /// <item>The test data instance itself when <see cref="ArgsCode.Instance"/></item>
    /// <item>The test case properties when <see cref="ArgsCode.Properties"/></item>
    /// </list>
    /// </returns>
    /// <exception cref="InvalidEnumargumentException">
    /// Thrown when an undefined <paramref name="ArgsCode"/> value is provided.
    /// </exception>
    protected virtual object?[] ToArgs(ArgsCode argsCode)
    => argsCode switch
    {
        ArgsCode.Instance => [this],
        ArgsCode.Properties => [TestCaseName],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };

    protected static object?[] Trim(
        Func<ArgsCode, PropsCode, object?[]> baseToParams,
        ArgsCode argsCode,
        PropsCode propsCode,
        bool propsCodeMatches)
    {
        var baseParams = baseToParams(argsCode, propsCode);
        var strategyMatches =
            argsCode == ArgsCode.Properties &&
            propsCodeMatches;

        return strategyMatches ?
            baseParams[1..]
            : baseParams;
    }

    protected static object?[] Extend<T>(
        Func<ArgsCode, object?[]> baseToArgs,
        ArgsCode argsCode,
        T? newArg)
    {
        var baseArgs = baseToArgs(argsCode);

        return argsCode switch
        {
            ArgsCode.Instance => baseArgs,
            ArgsCode.Properties => [.. baseArgs, newArg],
            _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
        };
    }
    #endregion
}
#endregion

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
: TestData(definition)
{
    private readonly string _result = result;
    public T1? Arg1 { get; init; } = arg1;

    public override sealed string GetResult()
    => GetOrSubstitute(_result, "result");

    public override sealed object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode)
    => Trim(base.ToParams, argsCode, propsCode,
        propsCode != PropsCode.TestCaseName);

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
