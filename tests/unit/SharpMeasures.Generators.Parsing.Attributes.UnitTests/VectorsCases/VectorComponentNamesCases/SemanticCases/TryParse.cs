namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorComponentNamesCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IVectorComponentNames? Target(ISemanticVectorComponentNamesParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticVectorComponentNamesParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_StringCollection(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Constructor_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Null(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Empty(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Names_Populated(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Names_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISemanticVectorComponentNamesParser parser) => IdenticalToExpected(parser, await VectorComponentNamesTestData.Expression_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticVectorComponentNamesParser parser, ITestData<IVectorComponentNames> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Names, actual.Names);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
    }
}
