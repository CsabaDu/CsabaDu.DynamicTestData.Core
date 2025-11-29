// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

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

    /// <summary>
    /// Determines equality with another <see cref="INamedTestCase"/> based on test case name comparison.
    /// </summary>
    /// <param name="other">The <see cref="INamedTestCase"/> to compare against.</param>
    /// <returns>
    /// <c>true</c> if the test case names match; otherwise <c>false</c>.
    /// </returns>
    public bool Equals(INamedTestCase? other)
    => other?.GetTestCaseName() == GetTestCaseName();

    /// <summary>
    /// Generates a hash code derived from the return value of the <see cref="GetTestCaseName"/> method.
    /// </summary>
    /// <returns>A stable hash code for the test case.</returns>
    public override int GetHashCode()
    => GetTestCaseName().GetHashCode();

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
        ArgsCode.Properties => [GetTestCaseName()],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };

    /// <summary>
    /// Converts the specified argument and property codes into an array of parameters,  and optionally retrieves the
    /// test case name.
    /// </summary>
    /// <param name="argsCode">Specifies the type of arguments to include in the resulting parameter array.</param>
    /// <param name="propsCode">Specifies the type of properties to include in the resulting parameter array.</param>
    /// <param name="testCaseName">When this method returns, contains the name of the test case, or <see langword="null"/>  if no test case name is
    /// available. This parameter is passed uninitialized.</param>
    /// <returns>An array of objects representing the parameters based on the specified <paramref name="argsCode"/>  and
    /// <paramref name="propsCode"/>.</returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when invalid enum values are provided.
    /// </exception>
    public object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode,
        out string testCaseName)
    {
        testCaseName = GetTestCaseName();
        return ToParams(argsCode, propsCode);
    }

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
        const int Idx_Expected = (int)PropsCode.Expected;
        var args = ToArgs(argsCode);

        return argsCode switch
        {
            ArgsCode.Instance => args,
            ArgsCode.Properties => propsCode switch
            {
                // For MSTest: include the test case name so
                // DynamicDataAttribute.DynamicDataDisplayName can use
                // TestDataFactory.GetDisplayName to construct the display name.
                PropsCode.TestCaseName => args,

                // Most common case: exclude test case name from args
                PropsCode.Expected => argsWithoutTestCaseName(),

                // Useful for NUnit/TestNG style tests returning values
                PropsCode.Returns => argsWithoutExpectedIf(this is ITestDataReturns),
                PropsCode.Throws => argsWithoutExpectedIf(this is ITestDataThrows),

                _ => throw propsCode.GetInvalidEnumArgumentException(nameof(propsCode)),
            },
            _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
        };

        #region Local methods
        object?[] argsWithoutExpectedIf(bool typeMatches)
        => typeMatches ?
            argsFrom(Idx_Expected + 1)
            : argsWithoutTestCaseName();

        object?[] argsWithoutTestCaseName()
        => argsFrom(Idx_Expected);

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
    => GetTestCaseName();

    #region Abstract methods
    /// <summary>
    /// Gets the unique name identifying this test case.
    /// </summary>
    public abstract string GetTestCaseName();
    #endregion
    #endregion

    #region Helper members
    /// <summary>
    /// Error message for insufficient test data properties.
    /// </summary>
    protected const string PropsCountNotEnoughMessage =
        "Insufficient PropsCode for the requested operation.";

    /// <summary>
    /// Constructs a standardized test case name by combining the test definition with its result.
    /// </summary>
    /// <param name="result">The test result or outcome to append to the definition.</param>
    /// <returns>
    /// A formatted test case name in the format: "{Definition} => {result}".
    /// If the Definition is null or whitespace, uses the literal "Definition" as the definition.
    /// </returns>
    /// <remarks>
    /// This method ensures consistent naming across all test cases by:
    /// <list type="bullet">
    ///   <item>Handling null/empty definitions gracefully</item>
    ///   <item>Providing a clear visual separator (" => ") between definition and result</item>
    ///   <item>Maintaining a predictable format for test reporting</item>
    /// </list>
    /// </remarks>
    protected string GetTestCaseName(string result)
    {
        result = string.IsNullOrWhiteSpace(result) ?
            nameof(result)
            : result;

        string definition = string.IsNullOrWhiteSpace(Definition) ?
            nameof(Definition)
            : Definition;

        return $"{definition} => {result}";
    }
    #endregion
}
#endregion

#region Concrete types
/// <summary>
/// Test data implementation for test cases with 1 type argument.
/// </summary>
/// <typeparam name="T1">Type of the first test argument.</typeparam>
/// <param name="Definition">Description of the test scenario.</param>
/// <param name="Expected">The expected result description.</param>
/// <param name="Arg1">First test argument value.</param>
public record TestData<T1>(
    string Definition,
    string Expected,
    T1? Arg1)
: TestData(Definition),
ITestData<string, T1>
{
    /// <summary>
    /// Gets the formatted test case name combining definition and expected result.
    /// </summary>
    public string TestCaseName
    => GetTestCaseName(
        string.IsNullOrWhiteSpace(Expected) ?
            nameof(Expected)
            : Expected);

    /// <inheritdoc/>
    public override sealed string GetTestCaseName()
    => TestCaseName;

    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg1);
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
: TestData<T1>(Definition, Expected, Arg1),
ITestData<string, T1, T2>
{
    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg2);
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
: TestData<T1, T2>(Definition, Expected, Arg1, Arg2),
ITestData<string, T1, T2, T3>
{
    /// <inheritdoc/>
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg3);
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
    Arg1, Arg2, Arg3),
    ITestData<string, T1, T2, T3, T4>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg4);
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
    Arg1, Arg2, Arg3, Arg4),
    ITestData<string, T1, T2, T3, T4, T5>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg5);
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
    Arg1, Arg2, Arg3, Arg4, Arg5),
    ITestData<string, T1, T2, T3, T4, T5, T6>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg6);
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
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6),
    ITestData<string, T1, T2, T3, T4, T5, T6, T7>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg7);
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
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7),
    ITestData<string, T1, T2, T3, T4, T5, T6, T7, T8>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg8);
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
    Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8),
    ITestData<string, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    /// <inheritdoc cref="TestData.ToArgs(ArgsCode)" />
    public override object?[] ToArgs(ArgsCode argsCode)
    => base.ToArgs(argsCode).Add(argsCode, Arg9);
}
#endregion
