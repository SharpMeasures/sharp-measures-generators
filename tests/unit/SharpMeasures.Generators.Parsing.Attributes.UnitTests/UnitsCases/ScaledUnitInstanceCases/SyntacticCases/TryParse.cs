namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.ScaledUnitInstanceCases.SyntacticCases;

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
    private static ISyntacticScaledUnitInstance? Target(ISyntacticScaledUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticScaledUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticScaledUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_Double(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Constructor_String_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Null(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_Empty(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OriginalUnitInstance_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.OriginalUnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleScale(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.DoubleScale);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_Null(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_Empty(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringScale_String(ISyntacticScaledUnitInstanceParser parser) => IdenticalToExpected(parser, await ScaledUnitInstanceTestData.StringScale_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticScaledUnitInstanceParser parser, ITestData<ISyntacticScaledUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.OriginalUnitInstance, actual.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Scale, actual.Scale);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.PluralForm, actual.Syntax.PluralForm);
        Assert.Equal(data.ExpectedResult.Syntax.OriginalUnitInstance, actual.Syntax.OriginalUnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.Scale, actual.Syntax.Scale);
    }
}
