namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorComponentNamesCases.SyntacticCases;

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
    private static ISyntacticVectorComponentNames? Target(ISyntacticVectorComponentNamesParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticVectorComponentNamesParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticVectorComponentNamesParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_StringCollection(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Constructor_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Null(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Empty(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Populated(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISyntacticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticVectorComponentNamesParser parser, ITestData<ISyntacticVectorComponentNames> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Names, actual.Names);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.NamesCollection, actual.Syntax.NamesCollection);
        Assert.Equal(data.ExpectedResult.Syntax.NamesElements, actual.Syntax.NamesElements);
        Assert.Equal(data.ExpectedResult.Syntax.Expression, actual.Syntax.Expression);
    }
}
