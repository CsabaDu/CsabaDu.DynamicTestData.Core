// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Core.DataStrategyTypes;
using CsabaDu.DynamicTestData.Core.TestDataTypes.Core;
using CsabaDu.DynamicTestData.Core.TestDataTypes.PatternMarkers.Interfaces;

namespace CsabaDu.DynamicTestData.Core.TestDataTypes.PatternMarkers;

public abstract class TestDataExpected<TResult>(
    string definition,
    TResult expected)
: TestDataBase(definition),
IExpected<TResult>
where TResult : notnull
{
    private const string ExpectedString = "expected";
    private const string ResultsString = "results";

    public TResult Expected { get; init; } = expected;

    public abstract string GetResultPrefix();

    public object GetExpected()
    => Expected;

    protected override object?[] ToArgs(ArgsCode argsCode)
    => Extend(base.ToArgs, argsCode, Expected);

    protected string GetExpectedResult(string? expectedString)
    {
        var resultPrefix = GetResultPrefix();
        var expected = GetOrSubstitute(expectedString, ExpectedString);

        return $"{resultPrefix} {expected}";
    }

    protected string GetResultPrefix(string resultPrefix)
    => GetOrSubstitute(resultPrefix, ResultsString);

    public override object?[] ToParams(
        ArgsCode argsCode,
        PropsCode propsCode)
    => Trim(base.ToParams, argsCode, propsCode,
        propsCode != PropsCode.TestCaseName);
}
