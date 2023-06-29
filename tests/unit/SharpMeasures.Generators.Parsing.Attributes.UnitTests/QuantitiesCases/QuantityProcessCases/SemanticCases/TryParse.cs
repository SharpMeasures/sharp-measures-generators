namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityProcessCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantityProcess? Target(ISemanticQuantityProcessParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantityProcessParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String_TypeCollection(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String_TypeCollection_StringCollection(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Constructor_Type_String_String_TypeCollection_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Expression_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Null(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Empty(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Populated(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.Signature_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Null(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Empty(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ParameterNames_Populated(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ParameterNames_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ImplementStatically_True(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ImplementStatically_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task ImplementStatically_False(ISemanticQuantityProcessParser parser) => IdenticalToExpected(parser, await QuantityProcessTestData.ImplementStatically_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantityProcessParser parser, ITestData<IQuantityProcess> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Result, actual.Result, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
        Assert.Equal(data.ExpectedResult.Signature, actual.Signature, ReferenceTypeSymbolComparer.CollectionComparer);
        Assert.Equal(data.ExpectedResult.ParameterNames, actual.ParameterNames);
        Assert.Equal(data.ExpectedResult.ImplementStatically, actual.ImplementStatically);
    }
}
