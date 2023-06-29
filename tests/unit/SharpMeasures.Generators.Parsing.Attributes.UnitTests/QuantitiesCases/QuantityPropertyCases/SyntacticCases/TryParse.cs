namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityPropertyCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticQuantityProperty? Target(ISyntacticQuantityPropertyParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticQuantityPropertyParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticQuantityPropertyParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Constructor_Type_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISyntacticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticQuantityPropertyParser parser, ITestData<ISyntacticQuantityProperty> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Result, actual.Result, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);

        Assert.Equal(data.ExpectedResult.Syntax.Result, actual.Syntax.Result);

        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.Expression, actual.Syntax.Expression);
    }
}
