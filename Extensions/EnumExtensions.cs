// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.Core.Extensions;

/// <summary>
/// Provides extension methods for validating and handling enumeration values in a type-safe manner.
/// </summary>
/// <remarks>The <see cref="EnumExtensions"/> class offers utility methods to ensure that enumeration values are
/// valid and to standardize exception handling for invalid enum arguments. These methods are intended to simplify
/// validation logic and promote consistent error reporting when working with strongly typed enums.</remarks>
public static class EnumExtensions
{
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
}
