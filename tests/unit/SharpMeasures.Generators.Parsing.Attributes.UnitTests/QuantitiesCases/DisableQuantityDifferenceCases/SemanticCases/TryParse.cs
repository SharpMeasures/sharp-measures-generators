namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantityDifferenceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IDisableQuantityDifference? Target(ISemanticDisableQuantityDifferenceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticDisableQuantityDifferenceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticDisableQuantityDifferenceParser parser) => IdenticalToExpected(parser, await DisableQuantityDifferenceTestData.Constructor_Empty);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticDisableQuantityDifferenceParser parser, ITestData<IDisableQuantityDifference> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);
    }
}
