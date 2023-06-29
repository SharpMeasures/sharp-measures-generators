namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorConstantCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IVectorConstant? Target(ISemanticVectorConstantParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticVectorConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_DoubleCollection(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Constructor_String_String_DoubleCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_StringCollection(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Constructor_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Empty(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Null(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Empty(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Populated(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Null(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Empty(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Populated(ISemanticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Populated);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticVectorConstantParser parser, ITestData<IVectorConstant> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Value, actual.Value, ValueEqualityComparer.Comparer);
    }
}
