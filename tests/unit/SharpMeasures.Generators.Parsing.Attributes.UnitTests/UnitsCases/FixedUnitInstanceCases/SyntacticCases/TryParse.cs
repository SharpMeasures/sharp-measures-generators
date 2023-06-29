namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.FixedUnitInstanceCases.SyntacticCases;

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
    private static ISyntacticFixedUnitInstance? Target(ISyntacticFixedUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticFixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticFixedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Constructor_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Constructor_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISyntacticFixedUnitInstanceParser parser) => IdenticalToExpected(parser, await FixedUnitInstanceTestData.PluralForm_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticFixedUnitInstanceParser parser, ITestData<ISyntacticFixedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.PluralForm, actual.Syntax.PluralForm);
    }
}
