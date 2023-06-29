namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityConversionCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantityConversion? Target(ISemanticQuantityConversionParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantityConversionParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_TypeCollection(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Constructor_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Null(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Quantities_Populated(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.Quantities_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsImplementation_Unrecognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsImplementation_Recognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsBehaviour_Unrecognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsBehaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsBehaviour_Recognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsBehaviour_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_Null(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsPropertyName_String(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsPropertyName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_Null(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsMethodName_String(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_Null(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ForwardsStaticMethodName_String(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.ForwardsStaticMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsImplementation_Unrecognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsImplementation_Recognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsBehaviour_Unrecognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsBehaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsBehaviour_Recognized(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsBehaviour_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_Null(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_Empty(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task BackwardsStaticMethodName_String(ISemanticQuantityConversionParser parser) => IdenticalToExpected(parser, await QuantityConversionTestData.BackwardsStaticMethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantityConversionParser parser, ITestData<IQuantityConversion> data)
    {
        var actual = Target(parser, data.AttributeData);

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
    }
}
