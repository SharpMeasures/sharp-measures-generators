namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.SpecializedVectorGroupCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISpecializedVectorGroup? Target(ISemanticSpecializedVectorGroupParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticSpecializedVectorGroupParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticSpecializedVectorGroupParser parser) => IdenticalToExpected(parser, await SpecializedVectorGroupTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticSpecializedVectorGroupParser parser, ITestData<ISpecializedVectorGroup> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Original, actual.Original, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
