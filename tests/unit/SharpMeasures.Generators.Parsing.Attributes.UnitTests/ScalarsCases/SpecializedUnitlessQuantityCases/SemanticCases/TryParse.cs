namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.SpecializedUnitlessQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISpecializedUnitlessQuantity? Target(ISemanticSpecializedUnitlessQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticSpecializedUnitlessQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticSpecializedUnitlessQuantityParser parser) => IdenticalToExpected(parser, await SpecializedUnitlessQuantityTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticSpecializedUnitlessQuantityParser parser, ITestData<ISpecializedUnitlessQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Original, actual.Original, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
