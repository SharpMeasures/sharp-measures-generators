namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorQuantityCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticVectorQuantity? Target(ISyntacticVectorQuantityParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticVectorQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticVectorQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticVectorQuantityParser parser) => IdenticalToExpected(parser, await VectorQuantityTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Dimension(ISyntacticVectorQuantityParser parser) => IdenticalToExpected(parser, await VectorQuantityTestData.Dimension);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticVectorQuantityParser parser, ITestData<ISyntacticVectorQuantity> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Unit, actual.Unit, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Dimension, actual.Dimension);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Unit, actual.Syntax.Unit);
        Assert.Equal(data.ExpectedResult.Syntax.Dimension, actual.Syntax.Dimension);
    }
}
