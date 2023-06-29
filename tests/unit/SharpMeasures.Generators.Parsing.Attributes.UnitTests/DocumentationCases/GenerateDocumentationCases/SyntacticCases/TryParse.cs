namespace SharpMeasures.Generators.Parsing.Attributes.DocumentationCases.GenerateDocumentationCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Documentation;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticGenerateDocumentation? Target(ISyntacticGenerateDocumentationParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticGenerateDocumentationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticGenerateDocumentationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISyntacticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Bool(ISyntacticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Constructor_Bool);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Generate_True(ISyntacticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Generate_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Generate_False(ISyntacticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Generate_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticGenerateDocumentationParser parser, ITestData<ISyntacticGenerateDocumentation> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Generate, actual.Generate);

        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Generate, actual.Syntax.Generate);
    }
}
