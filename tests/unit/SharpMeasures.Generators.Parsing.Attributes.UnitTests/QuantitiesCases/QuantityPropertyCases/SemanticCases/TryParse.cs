namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityPropertyCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantityProperty? Target(ISemanticQuantityPropertyParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantityPropertyParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_String_String(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Constructor_Type_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISemanticQuantityPropertyParser parser) => IdenticalToExpected(parser, await QuantityPropertyTestData.Expression_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantityPropertyParser parser, ITestData<IQuantityProperty> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Result, actual.Result, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
    }
}
