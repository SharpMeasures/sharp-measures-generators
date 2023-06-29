namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarQuantityCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IScalarQuantity? Target(ISemanticScalarQuantityParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticScalarQuantityParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Biased_True(ISemanticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Biased_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Biased_False(ISemanticScalarQuantityParser parser) => IdenticalToExpected(parser, await ScalarQuantityTestData.Biased_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticScalarQuantityParser parser, ITestData<IScalarQuantity> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Unit, actual.Unit, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Biased, actual.Biased);
    }
}
