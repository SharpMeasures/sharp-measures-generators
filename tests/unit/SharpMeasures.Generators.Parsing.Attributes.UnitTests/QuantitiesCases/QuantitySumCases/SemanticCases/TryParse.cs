namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantitySumCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantitySum? Target(ISemanticQuantitySumParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantitySumParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticQuantitySumParser parser) => IdenticalToExpected(parser, await QuantitySumTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantitySumParser parser, ITestData<IQuantitySum> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Sum, actual.Sum, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
