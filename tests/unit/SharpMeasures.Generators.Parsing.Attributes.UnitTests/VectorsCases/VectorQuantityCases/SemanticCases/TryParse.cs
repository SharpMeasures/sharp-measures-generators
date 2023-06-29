namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IVectorQuantity? Target(ISemanticVectorQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticVectorQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticVectorQuantityParser parser) => IdenticalToExpected(parser, await VectorQuantityTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Dimension(ISemanticVectorQuantityParser parser) => IdenticalToExpected(parser, await VectorQuantityTestData.Dimension);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticVectorQuantityParser parser, ITestData<IVectorQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Unit, actual.Unit, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Dimension, actual.Dimension);
    }
}
