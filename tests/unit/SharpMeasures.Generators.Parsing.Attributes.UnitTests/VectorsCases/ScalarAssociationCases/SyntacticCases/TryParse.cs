namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.ScalarAssociationCases.SyntacticCases;

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
    private static ISyntacticScalarAssociation? Target(ISyntacticScalarAssociationParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticScalarAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticScalarAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsComponents_True(ISyntacticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsComponents_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsComponents_False(ISyntacticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsComponents_False);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsMagnitude_True(ISyntacticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsMagnitude_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsMagnitude_False(ISyntacticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsMagnitude_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticScalarAssociationParser parser, ITestData<ISyntacticScalarAssociation> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.ScalarQuantity, actual.ScalarQuantity, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.AsComponents, actual.AsComponents);
        Assert.Equal(data.ExpectedResult.AsMagnitude, actual.AsMagnitude);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.ScalarQuantity, actual.Syntax.ScalarQuantity);
        Assert.Equal(data.ExpectedResult.Syntax.AsComponents, actual.Syntax.AsComponents);
        Assert.Equal(data.ExpectedResult.Syntax.AsMagnitude, actual.Syntax.AsMagnitude);
    }
}
