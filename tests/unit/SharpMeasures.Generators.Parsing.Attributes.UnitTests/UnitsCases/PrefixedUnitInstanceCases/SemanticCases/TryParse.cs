namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.PrefixedUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IPrefixedUnitInstance? Target(ISemanticPrefixedUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticPrefixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_MetricPrefixName(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_MetricPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_MetricPrefixName(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_String_MetricPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_BinaryPrefixName(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_BinaryPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_BinaryPrefixName(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_String_BinaryPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MetricPrefix_Unrecognized(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.MetricPrefix_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MetricPrefix_Recognized(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.MetricPrefix_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BinaryPrefix_Unrecognized(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.BinaryPrefix_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BinaryPrefix_Recognized(ISemanticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.BinaryPrefix_Recognized);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticPrefixedUnitInstanceParser parser, ITestData<IPrefixedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Prefix, actual.Prefix);
    }
}
