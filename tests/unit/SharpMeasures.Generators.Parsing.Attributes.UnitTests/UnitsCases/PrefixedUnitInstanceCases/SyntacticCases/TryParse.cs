namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.PrefixedUnitInstanceCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.Parsing.Attributes.UnitsCases.PrefixedUnitInstanceCases;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticPrefixedUnitInstance? Target(ISyntacticPrefixedUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticPrefixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticPrefixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_MetricPrefixName(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_MetricPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_MetricPrefixName(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_String_MetricPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_BinaryPrefixName(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_BinaryPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_BinaryPrefixName(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Constructor_String_String_String_BinaryPrefixName);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MetricPrefix_Unrecognized(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.MetricPrefix_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MetricPrefix_Recognized(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.MetricPrefix_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BinaryPrefix_Unrecognized(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.BinaryPrefix_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BinaryPrefix_Recognized(ISyntacticPrefixedUnitInstanceParser parser) => IdenticalToExpected(parser, await PrefixedUnitInstanceTestData.BinaryPrefix_Recognized);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticPrefixedUnitInstanceParser parser, ITestData<ISyntacticPrefixedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Prefix, actual.Prefix);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.PluralForm, actual.Syntax.PluralForm);
        Assert.Equal(data.ExpectedResult.Syntax.OriginalUnitInstance, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.Prefix, actual.Syntax.Prefix);
    }
}
