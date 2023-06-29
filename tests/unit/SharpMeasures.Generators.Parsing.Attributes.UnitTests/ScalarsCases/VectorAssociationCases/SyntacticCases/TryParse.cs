namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.VectorAssociationCases.SyntacticCases;

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
    private static ISyntacticVectorAssociation? Target(ISyntacticVectorAssociationParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticVectorAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticVectorAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticVectorAssociationParser parser) => IdenticalToExpected(parser, await VectorAssociationTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticVectorAssociationParser parser, ITestData<ISyntacticVectorAssociation> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.VectorQuantity, actual.VectorQuantity, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.VectorQuantity, actual.Syntax.VectorQuantity);
    }
}
