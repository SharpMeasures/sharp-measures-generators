namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.FixedUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IFixedUnitInstance? Target(ISemanticFixedUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticFixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Constructor_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticFixedUnitInstanceParser parser, ITestData<IFixedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
    }
}
