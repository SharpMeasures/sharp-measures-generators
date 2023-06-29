namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.ScaledUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IScaledUnitInstance? Target(ISemanticScaledUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticScaledUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_Double(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleScale(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.DoubleScale);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_Null(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_Empty(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_String(ISemanticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticScaledUnitInstanceParser parser, ITestData<IScaledUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Scale, actual.Scale);
    }
}
