// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Statics;

public static class Extensions
{
    #region object?[]
    /// <summary>
    /// Conditionally extends an arguments array based on the specified <see cref="ArgsCode"/> strategy.
    /// </summary>
    /// <typeparam name="T">The type of parameter to potentially add.</typeparam>
    /// <param name="args">The source arguments array.</param>
    /// <param name="argsCode">Determines the processing strategy:
    /// <list type="bullet">
    ///   <item><see cref="ArgsCode.Instance"/>: Returns the original array reference</item>
    ///   <item><see cref="ArgsCode.Properties"/>: Returns a new array with the parameter appended</item>
    /// </list>
    /// </param>
    /// <param name="parameter">The value to potentially append.</param>
    /// <returns>
    /// Either:
    /// <list type="bullet">
    ///   <item>The original <paramref name="args"/> array (when argsCode is Instance)</item>
    ///   <item>A new array containing existing elements plus <paramref name="parameter"/> (when argsCode is Properties)</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="argsCode"/> is neither Instance nor Properties.
    /// </exception>
    /// <remarks>
    /// Important behavior notes:
    /// <list type="bullet">
    ///   <item>For <see cref="ArgsCode.Instance"/>: Returns the original array reference without modification</item>
    ///   <item>For <see cref="ArgsCode.Properties"/>: Creates and returns a new array instance, with the specified parameter added.</item>
    ///   <item>Null <paramref name="args"/> will throw NullReferenceException</item>
    /// </list>
    /// </remarks>
    public static object?[] Add<T>(
        this object?[] args,
        ArgsCode argsCode,
        T? parameter)
    => argsCode switch
    {
        ArgsCode.Instance => args,
        ArgsCode.Properties => [.. args, parameter],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };
    #endregion

    #region ArgsCode
    /// <summary>
    /// Validates that the <see cref="ArgsCode"/> value is defined in the enumeration.
    /// This is typically used to ensure valid strategy configuration in <see cref="IDataStrategy"/>.
    /// </summary>
    /// <param name="argsCode">The argument code to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <returns>The original <paramref name="argsCode"/> if it is defined.</returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when the <paramref name="argsCode"/> is not a defined value in the <see cref="ArgsCode"/> enumeration.
    /// </exception>
    public static ArgsCode Defined(
        this ArgsCode argsCode,
        string paramName)
    => Enum.IsDefined(argsCode) ?
        argsCode
        : throw argsCode.GetInvalidEnumArgumentException(paramName);

    /// <summary>
    /// Creates a standardized invalid enumeration exception for <see cref="ArgsCode"/> values.
    /// Used throughout the test data framework to maintain consistent error reporting.
    /// </summary>
    /// <param name="argsCode">The invalid argument code value.</param>
    /// <param name="paramName">The name of the parameter that contained the invalid value.</param>
    /// <returns>A new <see cref="InvalidEnumArgumentException"/> instance.</returns>
    public static InvalidEnumArgumentException GetInvalidEnumArgumentException(
        this ArgsCode argsCode,
        string paramName)
    => new(paramName, (int)argsCode, typeof(ArgsCode));
    #endregion

    #region PropsCode
    /// <summary>
    /// Validates that the <see cref="PropsCode"/> value is defined in the enumeration.
    /// This ensures proper property filtering behavior in <see cref="IDataStrategy"/> implementations.
    /// </summary>
    /// <param name="propsCode">The property code to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <returns>The original <paramref name="propsCode"/> if it is defined.</returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when the <paramref name="propsCode"/> is not a defined value in the <see cref="PropsCode"/> enumeration.
    /// </exception>
    public static PropsCode Defined(
        this PropsCode propsCode,
        string paramName)
    => Enum.IsDefined(propsCode) ?
        propsCode
        : throw propsCode.GetInvalidEnumArgumentException(paramName);

    /// <summary>
    /// Creates a standardized invalid enumeration exception for <see cref="PropsCode"/> values.
    /// Used to maintain consistent error handling across the test data framework.
    /// </summary>
    /// <param name="propsCode">The invalid property code value.</param>
    /// <param name="paramName">The name of the parameter that contained the invalid value.</param>
    /// <returns>A new <see cref="InvalidEnumArgumentException"/> instance.</returns>
    public static InvalidEnumArgumentException GetInvalidEnumArgumentException(
        this PropsCode propsCode,
        string paramName)
    => new(paramName, (int)propsCode, typeof(PropsCode));
    #endregion
}