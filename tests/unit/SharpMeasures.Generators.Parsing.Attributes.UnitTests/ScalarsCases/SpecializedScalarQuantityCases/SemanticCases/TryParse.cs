namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.SpecializedScalarQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISpecializedScalarQuantity? Target(ISemanticSpecializedScalarQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticSpecializedScalarQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticSpecializedScalarQuantityParser parser) => IdenticalToExpected(parser, await SpecializedScalarQuantityTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticSpecializedScalarQuantityParser parser, ITestData<ISpecializedScalarQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Original, actual.Original, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
