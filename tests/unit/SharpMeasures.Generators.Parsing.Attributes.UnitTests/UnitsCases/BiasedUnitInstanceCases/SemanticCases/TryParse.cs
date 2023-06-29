namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.BiasedUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IBiasedUnitInstance? Target(ISemanticBiasedUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticBiasedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_Double(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleBias(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.DoubleBias);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_Null(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_Empty(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_String(ISemanticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticBiasedUnitInstanceParser parser, ITestData<IBiasedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Bias, actual.Bias);
    }
}
