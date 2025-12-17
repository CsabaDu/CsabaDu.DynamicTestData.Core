// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.Converters;

public static class TestDataConverter
{
    public static object?[] ReturnsToParams<TTestData>(
        this TTestData testData,
        ArgsCode argsCode)
    where TTestData : notnull, ITestData
    => testData.ToParams(argsCode, PropsCode.Returns);

    public static Type[]? GetTypeArgs<TTestData>(
        this TTestData testData,
        ArgsCode argsCode)
    where TTestData : notnull, ITestData
    {
        var testDataType = typeof(TTestData);

        return argsCode switch
        {
            ArgsCode.Instance => [testDataType],
            ArgsCode.Properties => getGenericTypeArgs(),
            _ => null,
        };

        #region Local methods
        Type[] getGenericTypeArgs()
        {
            var genericTypeArgs =
                testDataType.GetGenericArguments();

            return testData is IExpected ?
                genericTypeArgs[1..]
                : genericTypeArgs;
        }
        #endregion
    }
}