namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorGroupMemberCases.SyntacticCases;

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
    private static ISyntacticVectorGroupMember? Target(ISyntacticVectorGroupMemberParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticVectorGroupMemberParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticVectorGroupMemberParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISyntacticVectorGroupMemberParser parser) => IdenticalToExpected(parser, await VectorGroupMemberTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Dimension(ISyntacticVectorGroupMemberParser parser) => IdenticalToExpected(parser, await VectorGroupMemberTestData.Dimension);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticVectorGroupMemberParser parser, ITestData<ISyntacticVectorGroupMember> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Group, actual.Group, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Dimension, actual.Dimension);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Group, actual.Syntax.Group);
        Assert.Equal(data.ExpectedResult.Syntax.Dimension, actual.Syntax.Dimension);
    }
}
