// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.Extensions;

public static class ObjectArrayExtensions
{
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
}