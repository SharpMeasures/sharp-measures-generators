namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.AliasedUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IAliasedUnitInstance? Target(ISemanticAliasedUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticAliasedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.Constructor_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISemanticAliasedUnitInstanceParser parser) => IdenticalToExpected(parser, await AliasedUnitInstanceTestData.OriginalUnitInstance_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticAliasedUnitInstanceParser parser, ITestData<IAliasedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
    }
}
