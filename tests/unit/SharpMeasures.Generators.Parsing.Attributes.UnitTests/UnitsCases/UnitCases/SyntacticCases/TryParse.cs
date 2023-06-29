namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticUnit? Target(ISyntacticUnitParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticUnitParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticUnitParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BiasTerm_True(ISyntacticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.BiasTerm_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BiasTerm_False(ISyntacticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.BiasTerm_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticUnitParser parser, ITestData<ISyntacticUnit> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.ScalarQuantity, actual.ScalarQuantity, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.BiasTerm, actual.BiasTerm);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.ScalarQuantity, actual.Syntax.ScalarQuantity);
        Assert.Equal(data.ExpectedResult.Syntax.BiasTerm, actual.Syntax.BiasTerm);
    }
}
