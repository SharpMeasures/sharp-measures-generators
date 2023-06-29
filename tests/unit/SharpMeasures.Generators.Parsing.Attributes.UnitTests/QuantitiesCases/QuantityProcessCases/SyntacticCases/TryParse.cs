namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityProcessCases.SyntacticCases;

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
    private static ISyntacticQuantityProcess? Target(ISyntacticQuantityProcessParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticQuantityProcessParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticQuantityProcessParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String_TypeCollection(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String_TypeCollection_StringCollection(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String_TypeCollection_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Null(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Empty(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Populated(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Null(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Empty(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Populated(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ImplementStatically_True(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ImplementStatically_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ImplementStatically_False(ISyntacticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ImplementStatically_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticQuantityProcessParser parser, ITestData<ISyntacticQuantityProcess> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Result, actual.Result, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
        Assert.Equal(data.ExpectedResult.Signature, actual.Signature, ReferenceTypeSymbolComparer.CollectionComparer);
        Assert.Equal(data.ExpectedResult.ParameterNames, actual.ParameterNames);
        Assert.Equal(data.ExpectedResult.ImplementStatically, actual.ImplementStatically);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);

        Assert.Equal(data.ExpectedResult.Syntax.Result, actual.Syntax.Result);

        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.Expression, actual.Syntax.Expression);
        Assert.Equal(data.ExpectedResult.Syntax.SignatureCollection, actual.Syntax.SignatureCollection);
        Assert.Equal(data.ExpectedResult.Syntax.SignatureElements, actual.Syntax.SignatureElements);
        Assert.Equal(data.ExpectedResult.Syntax.ParameterNamesCollection, actual.Syntax.ParameterNamesCollection);
        Assert.Equal(data.ExpectedResult.Syntax.ParameterNamesElements, actual.Syntax.ParameterNamesElements);
        Assert.Equal(data.ExpectedResult.Syntax.ImplementStatically, actual.Syntax.ImplementStatically);
    }
}
