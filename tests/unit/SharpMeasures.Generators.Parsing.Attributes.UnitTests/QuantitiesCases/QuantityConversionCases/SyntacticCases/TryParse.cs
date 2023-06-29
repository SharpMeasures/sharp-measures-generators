namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityConversionCases.SyntacticCases;

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
    private static ISyntacticQuantityConversion? Target(ISyntacticQuantityConversionParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticQuantityConversionParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticQuantityConversionParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_TypeCollection(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Constructor_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Null(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Populated(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsImplementation_Unrecognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsImplementation_Recognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsBehaviour_Unrecognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsBehaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsBehaviour_Recognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsBehaviour_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_Null(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_String(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_Null(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_String(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_Null(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_String(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsImplementation_Unrecognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsImplementation_Recognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsBehaviour_Unrecognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsBehaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsBehaviour_Recognized(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsBehaviour_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_Null(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_Empty(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_String(ISyntacticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticQuantityConversionParser parser, ITestData<ISyntacticQuantityConversion> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Quantities, actual.Quantities, ReferenceTypeSymbolComparer.CollectionComparer);

        Assert.Equal(data.ExpectedResult.ForwardsImplementation, actual.ForwardsImplementation);
        Assert.Equal(data.ExpectedResult.ForwardsBehaviour, actual.ForwardsBehaviour);
        Assert.Equal(data.ExpectedResult.ForwardsPropertyName, actual.ForwardsPropertyName);
        Assert.Equal(data.ExpectedResult.ForwardsMethodName, actual.ForwardsMethodName);
        Assert.Equal(data.ExpectedResult.ForwardsStaticMethodName, actual.ForwardsStaticMethodName);

        Assert.Equal(data.ExpectedResult.BackwardsImplementation, actual.BackwardsImplementation);
        Assert.Equal(data.ExpectedResult.BackwardsBehaviour, actual.BackwardsBehaviour);
        Assert.Equal(data.ExpectedResult.BackwardsStaticMethodName, actual.BackwardsStaticMethodName);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);

        Assert.Equal(data.ExpectedResult.Syntax.QuantitiesCollection, actual.Syntax.QuantitiesCollection);
        Assert.Equal(data.ExpectedResult.Syntax.QuantitiesElements, actual.Syntax.QuantitiesElements);

        Assert.Equal(data.ExpectedResult.Syntax.ForwardsImplementation, actual.Syntax.ForwardsImplementation);
        Assert.Equal(data.ExpectedResult.Syntax.ForwardsBehaviour, actual.Syntax.ForwardsBehaviour);
        Assert.Equal(data.ExpectedResult.Syntax.ForwardsPropertyName, actual.Syntax.ForwardsPropertyName);
        Assert.Equal(data.ExpectedResult.Syntax.ForwardsMethodName, actual.Syntax.ForwardsMethodName);
        Assert.Equal(data.ExpectedResult.Syntax.ForwardsStaticMethodName, actual.Syntax.ForwardsStaticMethodName);

        Assert.Equal(data.ExpectedResult.Syntax.BackwardsImplementation, actual.Syntax.BackwardsImplementation);
        Assert.Equal(data.ExpectedResult.Syntax.BackwardsBehaviour, actual.Syntax.BackwardsBehaviour);
        Assert.Equal(data.ExpectedResult.Syntax.BackwardsStaticMethodName, actual.Syntax.BackwardsStaticMethodName);
    }
}
