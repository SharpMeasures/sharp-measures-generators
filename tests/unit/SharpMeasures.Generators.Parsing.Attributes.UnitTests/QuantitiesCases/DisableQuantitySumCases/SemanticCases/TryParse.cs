namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantitySumCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IDisableQuantitySum? Target(ISemanticDisableQuantitySumParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticDisableQuantitySumParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticDisableQuantitySumParser parser) => IdenticalToExpected(parser, await DisableQuantitySumTestData.Constructor_Empty);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticDisableQuantitySumParser parser, ITestData<IDisableQuantitySum> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);
    }
}
