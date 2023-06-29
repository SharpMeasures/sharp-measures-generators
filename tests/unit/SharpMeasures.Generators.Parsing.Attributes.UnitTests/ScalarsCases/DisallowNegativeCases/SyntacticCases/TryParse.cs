namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.DisallowNegativeCases.SyntacticCases;

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
    private static ISyntacticDisallowNegative? Target(ISyntacticDisallowNegativeParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticDisallowNegativeParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticDisallowNegativeParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISyntacticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_DisallowNegativeBehaviour(ISyntacticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Constructor_DisallowNegativeBehaviour);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Behaviour_Unrecognized(ISyntacticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Behaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Behaviour_Recognized(ISyntacticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Behaviour_Recognized);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticDisallowNegativeParser parser, ITestData<ISyntacticDisallowNegative> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Behaviour, actual.Behaviour);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Behaviour, actual.Syntax.Behaviour);
    }
}
