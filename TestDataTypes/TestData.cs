// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Factories;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;
using CsabaDu.DynamicTestData.Core.Validators;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

#region Abstract type
/// <summary>
/// Abstract base record representing test case data with core functionality for test argument generation.
/// </summary>
/// <param name="Definition">The descriptive definition of the test case scenario.</param>
/// <remarks>
/// Provides foundational implementation for:
/// <list type="bullet">
/// <item>Test case naming and identification</item>
/// <item>Argument generation strategies</item>
/// <item>Equality comparison based on test case names</item>
/// <item>Conversion to parameter arrays for test execution</item>
/// </list>
/// </remarks>
public abstract record TestData(string Definition)
: ITestData
{
    #region Abstract properties
    /// <summary>
    /// Gets the unique name identifying this test case.
    /// </summary>
    public abstract string TestCaseName { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Determines whether the current instance is contained within the specified collection of named test cases.
    /// </summary>
    /// <param name="namedTestCases">The collection of <see cref="INamedTestCase"/> instances to search. Can be <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the current instance is found in the specified collection; otherwise, <see
    /// langword="false"/>.  Returns <see langword="false"/> if <paramref name="namedTestCases"/> is <see
    /// langword="null"/>.</returns>
    public bool ContainedBy(IEnumerable<INamedTestCase>? namedTestCases)
    => namedTestCases?.Any(Equals) == true;

    public string? GetDisplayName(string? testMethodName)
    => DisplayNameFactory.CreateDisplayName(
        testMethodName,
        TestCaseName);

    /// <summary>
    /// Determines equality with another <see cref="INamedTestCase"/> based on test case name comparison.
    /// </summary>
    /// <param name="other">The <see cref="INamedTestCase"/> to compare against.</param>
    /// <returns>
    /// <c>true</c> if the test case names match; otherwise <c>false</c>.
    /// </returns>
    public bool Equals(INamedTestCase? other)
    => other?.TestCaseName == TestCaseName;

    /// <summary>
    /// Generates a hash code derived from the return value of the <see cref="GetTestCaseName"/> method.
    /// </summary>
    /// <returns>A stable hash code for the test case.</returns>
    public override int GetHashCode()
    => TestCaseName.GetHashCode();

    /// <summary>
    /// Converts the test data to an argument array based on the specified <see cref="ArgsCode"/> parameter.
    /// </summary>
    /// <param name="argsCode">Determines whether to include the instance itself or its properties.</param>
    /// <returns>
    /// An array containing:
    /// <list type="bullet">
    /// <item>The test data instance itself when <see cref="ArgsCode.Instance"/></item>
    /// <item>The test case properties when <see cref="ArgsCode.Properties"/></item>
    /// </list>
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when an undefined <paramref name="argsCode"/> value is provided.
    /// </exception>
    public virtual object?[] ToArgs(ArgsCode argsCode)
    => argsCode switch
    {
        ArgsCode.Instance => [this],
        ArgsCode.Properties => [TestCaseName],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };

    public object?[] ToParams(ArgsCode argsCode)
    => ToParams(argsCode, PropsCode.Expected);

    /// <summary>
    /// Converts the test data to a parameter array with precise control over included properties.
    /// </summary>
    /// <param name="argsCode">Determines instance vs properties inclusion.</param>
    /// <param name="propsCode">Specifies which properties to include when using <see cref="ArgsCode.Properties"/>.</param>
    /// <returns>
    /// A parameter array tailored for test execution based on the specified codes.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when invalid enum values are provided.
    /// </exception>
    public object?[] ToParams(ArgsCode argsCode, PropsCode propsCode)
    {
        const int idx_Expected = (int)PropsCode.Expected;
        var args = ToArgs(argsCode);

        return argsCode switch
        {
            ArgsCode.Instance => args,
            ArgsCode.Properties => propsCode switch
            {
                // For MSTest: include the test case name so
                // DynamicDataAttribute.DynamicDataDisplayName can use
                // TestDataFactory.CreateDisplayName to construct the display name.
                PropsCode.TestCaseName => args,

                // Most common case: exclude test case name from args
                PropsCode.Expected => argsWithoutTestCaseName(),

                // Useful for NUnit/TestNG style tests returning values
                PropsCode.Returns => argsWithoutExpectedIf(this is IReturns),
                PropsCode.Throws => argsWithoutExpectedIf(this is IThrows),

                _ => throw propsCode.GetInvalidEnumArgumentException(nameof(propsCode)),
            },
            _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
        };

        #region Local methods
        object?[] argsWithoutExpectedIf(bool typeMatches)
        => typeMatches ?
            argsFrom(idx_Expected + 1)
            : argsWithoutTestCaseName();

        object?[] argsWithoutTestCaseName()
        => argsFrom(idx_Expected);

        object?[] argsFrom(int index)
        => args.Length > index ?
            args[index..]
            : throw new ArgumentOutOfRangeException(
                nameof(propsCode),
                PropsCountNotEnoughMessage);
        #endregion
    }

    /// <summary>
    /// Overrides and seals the `ToString()` method to return the value of <see cref="GetTestCaseName"/> method.
    /// </summary>
    public override sealed string ToString()
    => TestCaseName;
    #endregion

    #region Helper members
    /// <summary>
    /// Error message for insufficient test data properties.
    /// </summary>
    protected const string PropsCountNotEnoughMessage =
        "Insufficient PropsCode for the requested operation.";

    /// <summary>
    /// Constructs a standardized test case name by combining the test definition with its expectedToString.
    /// </summary>
    /// <param name="result">The test expectedToString or outcome to append to the definition.</param>
    /// <returns>
    /// A formatted test case name in the format: "{Definition} => {expectedToString}".
    /// If the Definition is null or whitespace, uses the literal "Definition" as the definition.
    /// </returns>
    /// <remarks>
    /// This method ensures consistent naming across all test cases by:
    /// <list type="bullet">
    ///   <item>Handling null/empty definitions gracefully</item>
    ///   <item>Providing a clear visual separator (" => ") between definition and expectedToString</item>
    ///   <item>Maintaining a predictable format for test reporting</item>
    /// </list>
    /// </remarks>
    protected string GetTestCaseName(string? result)
    {
        string substitute = nameof(Definition);
        string definition = validated(Definition);

        var expected = this as IExpected;
        bool isExpected = expected is not null;

        substitute = isExpected ? nameof(expected) : nameof(result);
        result = validated(result);

        return isExpected ?
            $"{definition} => {expected!.ResultPrefix} {result}"
            : $"{definition} => {result}";

        #region Local function
        string validated(string? value)
        => string.IsNullOrEmpty(value) ?
            substitute
            : value;
        #endregion
    }

    /// <summary>
    /// Conditionally extends an arguments array based on the specified <see cref="ArgsCode"/> strategy.
    /// </summary>
    /// <typeparam name="T">The type of newArg to potentially add.</typeparam>
    /// <param name="baseArgs">The source arguments array.</param>
    /// <param name="argsCode">Determines the processing strategy:
    /// <list type="bullet">
    ///   <item><see cref="ArgsCode.Instance"/>: Returns the original array reference</item>
    ///   <item><see cref="ArgsCode.Properties"/>: Returns a new array with the newArg appended</item>
    /// </list>
    /// </param>
    /// <param name="newArg">The value to potentially append.</param>
    /// <returns>
    /// Either:
    /// <list type="bullet">
    ///   <item>The original <paramref name="baseArgs"/> array (when argsCode is Instance)</item>
    ///   <item>A new array containing existing elements plus <paramref name="newArg"/> (when argsCode is Properties)</item>
    /// </list>
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when <paramref name="argsCode"/> is neither Instance nor Properties.
    /// </exception>
    /// <remarks>
    /// Important behavior notes:
    /// <list type="bullet">
    ///   <item>For <see cref="ArgsCode.Instance"/>: Returns the original array reference without modification</item>
    ///   <item>For <see cref="ArgsCode.Properties"/>: Creates and returns a new array instance, with the specified newArg added.</item>
    ///   <item>Null <paramref name="baseArgs"/> will throw NullReferenceException</item>
    /// </list>
    /// </remarks>
    protected static object?[] Extend<T>(object?[] baseArgs, T? newArg, ArgsCode argsCode)
    => argsCode switch
    {
        ArgsCode.Instance => baseArgs,
        ArgsCode.Properties => [.. baseArgs, newArg],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };
    #endregion
}
#endregion

#region Concrete types
/// <summary>
/// Test data implementation for test cases with 1 type argument.
/// </summary>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="Definition">Description of the test scenario.</param>
/// <param name="Expected">The result expectedToString description.</param>
/// <param name="Arg1">First test argument value.</param>
public record TestData<T1>(
    string Definition,
    string Expected,
    T1? Arg1)
: TestData(Definition)
{
    /// <inheritdoc/>
    public override sealed string TestCaseName
    => GetTestCaseName(Expected);

    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg1, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 2 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1}"/>
/// <typeparam name="T2">Type of the second test argument.</typeparam>
/// /// <param name="Arg2">Second test argument value.</param>

public record TestData<T1, T2>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2)
: TestData<T1>(Definition, Expected, Arg1)
{
    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg2, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 3 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2}"/>
/// <typeparam name="T3">Type of the third test argument.</typeparam>
/// /// <param name="Arg3">Third test argument value.</param>
public record TestData<T1, T2, T3>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3)
: TestData<T1, T2>(Definition, Expected, Arg1, Arg2)
{
    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg3, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 4 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3}" />
/// <typeparam name="T4">The type of the fourth argument.</typeparam>
/// <param name="Arg4">The fourth test argument value..</param>
public record TestData<T1, T2, T3, T4>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4)
: TestData<T1, T2, T3>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg4, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 5 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4}" />
/// <typeparam name="T5">The type of the fifth argument.</typeparam>
/// <param name="Arg5">The fifth test argument value..</param>
public record TestData<T1, T2, T3, T4, T5>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5)
: TestData<T1, T2, T3, T4>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg5, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 6 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5}" />
/// <typeparam name="T6">The type of the sixth argument.</typeparam>
/// <param name="Arg6">The sixth test argument value..</param>
public record TestData<T1, T2, T3, T4, T5, T6>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6)
: TestData<T1, T2, T3, T4, T5>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg6, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 7 type arguments.
/// </summary>
// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6}" />
/// <typeparam name="T7">The type of the seventh argument.</typeparam>
/// <param name="Arg7">The seventh test argument value..</param>
public record TestData<T1, T2, T3, T4, T5, T6, T7>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7)
: TestData<T1, T2, T3, T4, T5, T6>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg7, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 8 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6, T7}" />
/// <typeparam name="T8">The type of the eighth argument.</typeparam>
/// <param name="Arg8">The eighth test argument value..</param>
public record TestData<T1, T2, T3, T4, T5, T6, T7, T8>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8)
: TestData<T1, T2, T3, T4, T5, T6, T7>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg8, argsCode);
}

/// <summary>
/// Test data implementation for test cases with 9 type arguments.
/// </summary>
/// <inheritdoc cref="TestData{T1, T2, T3, T4, T5, T6, T7, T8}" />
/// <typeparam name="T9">The type of the ninth argument.</typeparam>
/// <param name="Arg9">The ninth test argument value..</param>
public record TestData<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    string Definition,
    string Expected,
    T1? Arg1, T2? Arg2, T3? Arg3, T4? Arg4, T5? Arg5, T6? Arg6, T7? Arg7, T8? Arg8, T9? Arg9)
: TestData<T1, T2, T3, T4, T5, T6, T7, T8>(
    Definition,
    Expected,
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8)
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs(argsCode), Arg9, argsCode);
}
#endregion
