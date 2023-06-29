namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarConstantCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IScalarConstant? Target(ISemanticScalarConstantParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticScalarConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Empty(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleValue(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.DoubleValue);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringValue_Null(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.StringValue_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringValue_Empty(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.StringValue_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringValue_String(ISemanticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.StringValue_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticScalarConstantParser parser, ITestData<IScalarConstant> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Value, actual.Value);
    }
}
