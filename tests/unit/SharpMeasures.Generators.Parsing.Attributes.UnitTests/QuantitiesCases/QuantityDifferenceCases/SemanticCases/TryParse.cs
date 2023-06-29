namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityDifferenceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantityDifference? Target(ISemanticQuantityDifferenceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantityDifferenceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticQuantityDifferenceParser parser) => IdenticalToExpected(parser, await QuantityDifferenceTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantityDifferenceParser parser, ITestData<IQuantityDifference> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Difference, actual.Difference, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
