// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.Statics;

public static class Extensions
{
    #region object?[]
    /// <summary>
    /// Conditionally extends an arguments array based on the specified <see cref="ArgsCode"/> strategy.
    /// </summary>
    /// <typeparam name="T">The type of newArg to potentially add.</typeparam>
    /// <param name="args">The source arguments array.</param>
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
    ///   <item>The original <paramref name="args"/> array (when argsCode is Instance)</item>
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
    ///   <item>Null <paramref name="args"/> will throw NullReferenceException</item>
    /// </list>
    /// </remarks>
    public static object?[] Add<T>(
        this object?[] args,
        ArgsCode argsCode,
        T? newArg)
    => argsCode switch
    {
        ArgsCode.Instance => args,
        ArgsCode.Properties => [.. args, newArg],
        _ => throw argsCode.GetInvalidEnumArgumentException(nameof(argsCode)),
    };
    #endregion

    #region Generic TEnum : struct, Enum
    /// <summary>
    /// Validates that the <see cref="enum"/> value is defined in the 'TEnum'-type enumeration.
    /// </summary>
    /// <param name="enumValue">The <see cref="enum"/>  value to validate.</param>
    /// <param name="paramName">The name of the newArg being validated.</param>
    /// <returns>The original <paramref name="propsCode"/> if it is defined.</returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// Thrown when the <paramref name="enumValue"/> is not a defined value in the 'TEnum' enumeration.
    /// </exception>
    public static TEnum Defined<TEnum>(
        this TEnum enumValue,
        string? paramName)
    where TEnum : struct, Enum
    => Enum.IsDefined(enumValue) ?
        enumValue
        : throw enumValue.GetInvalidEnumArgumentException(paramName);

    /// <summary>
    /// Creates a standardized invalid enumeration exception for 'TEnum'-type <see cref="enum"/> values.
    /// Used to maintain consistent error handling across the test data framework.
    /// </summary>
    /// <param name="enumValue">The invalid 'TEnum'-type <see cref="enum"/> value.</param>
    /// <param name="paramName">The name of the newArg that contained the invalid value.</param>
    /// <returns>A new <see cref="InvalidEnumArgumentException"/> instance.</returns>
    public static InvalidEnumArgumentException GetInvalidEnumArgumentException<TEnum>(
        this TEnum enumValue,
        string? paramName)
    where TEnum : struct, Enum
    => new(paramName, (int)(object)enumValue, typeof(TEnum));
    #endregion
}