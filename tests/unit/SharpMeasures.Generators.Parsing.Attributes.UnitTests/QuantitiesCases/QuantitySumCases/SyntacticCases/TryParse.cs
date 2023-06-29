namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantitySumCases.SyntacticCases;

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
    private static ISyntacticQuantitySum? Target(ISyntacticQuantitySumParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticQuantitySumParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticQuantitySumParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticQuantitySumParser parser) => IdenticalToExpected(parser, await QuantitySumTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticQuantitySumParser parser, ITestData<ISyntacticQuantitySum> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Sum, actual.Sum, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);

        Assert.Equal(data.ExpectedResult.Syntax.Sum, actual.Syntax.Sum);
    }
}
