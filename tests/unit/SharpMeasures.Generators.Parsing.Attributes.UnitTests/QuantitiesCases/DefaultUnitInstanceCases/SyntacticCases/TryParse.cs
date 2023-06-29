namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DefaultUnitInstanceCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticDefaultUnitInstance? Target(ISyntacticDefaultUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticDefaultUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticDefaultUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Constructor_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_EmptyString(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_EmptyString);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_Null(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_EmptyString(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_EmptyString);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Symbol_String(ISyntacticDefaultUnitInstanceParser parser) => IdenticalToExpected(parser, await DefaultUnitInstanceTestData.Symbol_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticDefaultUnitInstanceParser parser, ITestData<ISyntacticDefaultUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Symbol, actual.Symbol);

        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.UnitInstance, actual.Syntax.UnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.Symbol, actual.Syntax.Symbol);
    }
}
