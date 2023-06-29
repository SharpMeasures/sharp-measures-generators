namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DefaultUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IDefaultUnitInstance? Target(ISemanticDefaultUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticDefaultUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Constructor_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_EmptyString(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_EmptyString);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_Null(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_EmptyString(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_EmptyString);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_String(ISemanticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticDefaultUnitInstanceParser parser, ITestData<IDefaultUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Symbol, actual.Symbol);
    }
}
