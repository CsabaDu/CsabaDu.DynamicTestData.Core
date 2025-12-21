// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

/// <summary>
/// Core interface representing test data with basic test case functionality.
/// </summary>
/// <remarks>
/// Provides fundamental operations for:
/// <list type="bullet">
///   <item>Test case naming and identification (via <see cref="INamedTestCase"/>)</item>
///   <item>Test scenario definition</item>
///   <item>Argument generation for test execution</item>
/// </list>
/// </remarks>
public interface ITestData : INamedTestCase
{
    /// <summary>
    /// Gets the description of the test scenario being verified.
    /// </summary>
    string Definition { get; }

    /// <summary>
    /// Converts the test case to an array of arguments based on the specified <see cref="ArgsCode"> parameter.
    /// </summary>
    /// <param name="argsCode">Determines whether to include the test data instance or just its properties.</param>
    /// <returns>
    /// An array of arguments ready for test execution.
    /// </returns>
    object?[] ToArgs(ArgsCode argsCode);

    object?[] ToParams(ArgsCode argsCode);

    /// <summary>
    /// Converts the test case to parameters with precise control over included elements.
    /// </summary>
    /// <param name="argsCode">Determines instance vs properties inclusion.</param>
    /// <param name="propsCode">Specifies which properties to include.</param>
    /// <returns>
    /// A parameter array tailored for test execution.
    /// </returns>
    object?[] ToParams(ArgsCode argsCode, PropsCode propsCode);
}

/// <summary>
/// Represents a generic test data interface that extends <see cref="ITestData" with the generic type of the Expected non-nullable result of the test case.
/// </summary>
/// <typeparam name="TResult">The type of Expected result (non-nullable).</typeparam>
/// <remarks>
/// Extends <see cref="ITestData"/> with type-safe Expected result handling.
/// </remarks>
public interface ITestData<out TResult> : ITestData
    where TResult : notnull
{
    /// <summary>
    /// Gets the Expected result of the test case.
    /// </summary>
    TResult Expected { get; }
}

///// <summary>
///// Test data interface with one typed argument.
///// </summary>
///// <typeparam name="TResult">The type of Expected result (non-nullable).</typeparam>
///// <typeparam name="T1">The type of the first test argument.</typeparam>
///// <param name="Arg1">First test argument value.</param>
//public interface ITestData<out TResult, out T1> : ITestData<TResult>
//    where TResult : notnull
//{
//    /// <summary>
//    /// Gets the first argument value for the test case.
//    /// </summary>
//    T1? Arg1 { get; }
//}

///// <summary>
///// Test data interface with two typed arguments.
///// </summary>
///// <typeparam name="TResult">The type of Expected result (non-nullable).</typeparam>
///// <typeparam name="T1">The type of the first test argument.</typeparam>
///// <typeparam name="T2">The type of the second test argument.</typeparam>
//public interface ITestData<out TResult, out T1, out T2> : ITestData<TResult, T1>
//    where TResult : notnull
//{
//    /// <summary>
//    /// Gets the second argument value for the test case.
//    /// </summary>
//    T2? Arg2 { get; }
//}

///// <summary>
///// Test data interface with three typed arguments.
///// </summary>
///// <typeparam name="TResult">The type of Expected result (non-nullable).</typeparam>
///// <typeparam name="T1">The type of the first test argument.</typeparam>
///// <typeparam name="T2">The type of the second test argument.</typeparam>
///// <typeparam name="T3">The type of the third test argument.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3> : ITestData<TResult, T1, T2>
//    where TResult : notnull
//{
//    /// <summary>
//    /// Gets the third argument value for the test case.
//    /// </summary>
//    T3? Arg3 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3}" />
///// <typeparam name="T4">The fourth type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4>
//: ITestData<TResult, T1, T2, T3>
//where TResult : notnull
//{
//    /// <summary>
//    /// Gets the fourth argument of the test case.
//    /// </summary>
//    T4? Arg4 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3, T4}" />
///// <typeparam name="T5">The fifth type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4, out T5>
//: ITestData<TResult, T1, T2, T3, T4>
//where TResult : notnull
//{
//    /// <summary>
//    /// Gets the fifth argument of the test case.
//    /// </summary>
//    T5? Arg5 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3, T4, T5}" />
///// <typeparam name="T6">The sixth type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4, out T5, out T6>
//: ITestData<TResult, T1, T2, T3, T4, T5>
//where TResult : notnull
//{
//    /// <summary>
//    /// Gets the sixth argument of the test case.
//    /// </summary>
//    T6? Arg6 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3, T4, T5, T6}" />
///// <typeparam name="T7">The seventh type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4, out T5, out T6, out T7>
//: ITestData<TResult, T1, T2, T3, T4, T5, T6>
//where TResult : notnull
//{
//    /// <summary>
//    /// Gets the seventh argument of the test case.
//    /// </summary>
//    T7? Arg7 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3, T4, T5, T6, T7}" />
///// <typeparam name="T8">The eighth type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8>
//: ITestData<TResult, T1, T2, T3, T4, T5, T6, T7>
//where TResult : notnull
//{
//    /// <summary>
//    /// Gets the eighth argument of the test case.
//    /// </summary>
//    T8? Arg8 { get; }
//}

///// <inheritdoc cref="ITestData{TResult, T1, T2, T3, T4, T5, T6, T7, T8}" />
///// <typeparam name="T9">The ninth type of the test dataRows.</typeparam>
//public interface ITestData<out TResult, out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8, out T9>
//: ITestData<TResult, T1, T2, T3, T4, T5, T6, T7, T8> where TResult : notnull
//{
//    /// <summary>
//    /// Gets the ninth argument of the test case.
//    /// </summary>
//    T9? Arg9 { get; }
//}
