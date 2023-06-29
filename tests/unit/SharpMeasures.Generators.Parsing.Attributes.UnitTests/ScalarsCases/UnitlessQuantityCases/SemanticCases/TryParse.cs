namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.UnitlessQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IUnitlessQuantity? Target(ISemanticUnitlessQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticUnitlessQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticUnitlessQuantityParser parser) => IdenticalToExpected(parser, await UnitlessQuantityTestData.Constructor_Empty);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticUnitlessQuantityParser parser, ITestData<IUnitlessQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);
    }
}
