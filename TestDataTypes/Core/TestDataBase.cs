// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Core.Interfaces;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Identity;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Core;

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
public abstract class TestDataBase(string definition)
: NamedTestCase, ITestData
{
    #region Fields
    private readonly string _definition = definition;
    private const string DefinitionString = "definition";
    private const string Separator = " => ";
    private string? _cachedTestCaseName;
    #endregion

    #region Properties
    public override sealed string TestCaseName
    => LazyInitializer.EnsureInitialized(
        ref _cachedTestCaseName,
        CreateTestCaseName);
    #endregion

    #region Methods
    public string GetDefinition()
    => GetOrSubstitute(_definition, DefinitionString);

    /// <summary>
    /// Convenience overload of <see cref="ToParams(ArgsCode, PropsCode)"/> for the most common use case:
    /// use the <see cref="PropsCode.Expected"/> property selection.
    /// </summary>
    /// <param name="argsCode">Determines instance vs properties inclusion.</param>
    /// <returns></returns>
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
    public virtual object?[] ToParams(ArgsCode argsCode, PropsCode propsCode)
    {
        if (argsCode == ArgsCode.Properties)
        {
            _ = propsCode.Defined(nameof(propsCode));
        }

        return ToArgs(argsCode);
    }

    /// <inheritdoc />
    public abstract string GetResult();
    #endregion

    #region Helper methods
    private string CreateTestCaseName()
    {
        var definition = GetDefinition();
        var result = GetResult();
        var totalLength =
            definition.Length +
            Separator.Length +
            result.Length;

        return string.Create(
            totalLength,
            (definition, Separator, result),
            static (span, state) =>
            {
                var (d, s, r) = state;

                var index = 0;
                copy(d, span, index);

                index = d.Length;
                copy(s, span, index);

                index += s.Length;
                copy(r, span, index);
            });

        #region Local method
        static void copy(string part, Span<char> span, int index)
        => part.AsSpan().CopyTo(span[index..]);
        #endregion
    }

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

    public static string GetOrSubstitute(string? value, string substitute)
    => string.IsNullOrEmpty(value) ?
        substitute
        : value;
    #endregion
}