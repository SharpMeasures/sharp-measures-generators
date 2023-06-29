namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.BiasedUnitInstanceCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticBiasedUnitInstance? Target(ISyntacticBiasedUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticBiasedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticBiasedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_Double(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Constructor_String_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleBias(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.DoubleBias);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_Null(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_Empty(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringBias_String(ISyntacticBiasedUnitInstanceParser parser) => IdenticalToExpected(parser, await BiasedUnitInstanceTestData.StringBias_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticBiasedUnitInstanceParser parser, ITestData<ISyntacticBiasedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Bias, actual.Bias);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.PluralForm, actual.Syntax.PluralForm);
        Assert.Equal(data.ExpectedResult.Syntax.OriginalUnitInstance, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.Bias, actual.Syntax.Bias);
    }
}
