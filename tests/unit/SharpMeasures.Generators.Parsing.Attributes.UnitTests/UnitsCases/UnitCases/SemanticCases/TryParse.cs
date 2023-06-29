namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IUnit? Target(ISemanticUnitParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticUnitParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BiasTerm_True(ISemanticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.BiasTerm_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BiasTerm_False(ISemanticUnitParser parser) => IdenticalToExpected(parser, await UnitTestData.BiasTerm_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticUnitParser parser, ITestData<IUnit> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.ScalarQuantity, actual.ScalarQuantity, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.BiasTerm, actual.BiasTerm);
    }
}
