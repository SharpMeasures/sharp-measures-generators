namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.SpecializedVectorQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISpecializedVectorQuantity? Target(ISemanticSpecializedVectorQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticSpecializedVectorQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticSpecializedVectorQuantityParser parser) => IdenticalToExpected(parser, await SpecializedVectorQuantityTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticSpecializedVectorQuantityParser parser, ITestData<ISpecializedVectorQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Original, actual.Original, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
