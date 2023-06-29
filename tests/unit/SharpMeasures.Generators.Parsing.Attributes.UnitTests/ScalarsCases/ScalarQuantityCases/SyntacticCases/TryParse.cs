namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarQuantityCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticScalarQuantity? Target(ISyntacticScalarQuantityParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticScalarQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticScalarQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Biased_True(ISyntacticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Biased_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Biased_False(ISyntacticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Biased_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticScalarQuantityParser parser, ITestData<ISyntacticScalarQuantity> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Unit, actual.Unit, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Biased, actual.Biased);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Unit, actual.Syntax.Unit);
        Assert.Equal(data.ExpectedResult.Syntax.Biased, actual.Syntax.Biased);
    }
}
