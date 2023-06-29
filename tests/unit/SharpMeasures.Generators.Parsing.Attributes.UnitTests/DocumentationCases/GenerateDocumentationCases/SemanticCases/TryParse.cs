namespace SharpMeasures.Generators.Parsing.Attributes.DocumentationCases.GenerateDocumentationCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Documentation;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IGenerateDocumentation? Target(ISemanticGenerateDocumentationParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticGenerateDocumentationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Bool(ISemanticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Constructor_Bool);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Generate_True(ISemanticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Generate_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Generate_False(ISemanticGenerateDocumentationParser parser) => IdenticalToExpected(parser, await GenerateDocumentationTestData.Generate_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticGenerateDocumentationParser parser, ITestData<IGenerateDocumentation> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Generate, actual.Generate);
    }
}
