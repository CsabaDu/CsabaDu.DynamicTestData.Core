// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes;

public abstract class TestDataExpected<TResult>(
    string definition,
    TResult expected)
: TestData(definition),
IExpected<TResult>
where TResult : notnull
{
    public TResult Expected { get; init; } = expected;

    public abstract string ResultPrefix { get; }

    public object GetExpected()
    => Expected;

    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Expected);

    protected string GetResult(string? expectedString)
    {
        var expected = GetOrSubstitute(expectedString, "expected");

        return $"{ResultPrefix} {expected}";
    }

    public override object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode)
    => Trim(base.ToParams, argsCode, propsCode,
        propsCode != PropsCode.TestCaseName);
}
