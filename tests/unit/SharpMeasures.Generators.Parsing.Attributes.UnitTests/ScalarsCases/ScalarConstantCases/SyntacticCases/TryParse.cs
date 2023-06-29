namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarConstantCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticScalarConstant? Target(ISyntacticScalarConstantParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticScalarConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticScalarConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_Double(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Constructor_String_String_Double);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Constructor_String_String_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Empty(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleValue(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.DoubleValue);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringValue_Null(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.StringValue_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringValue_Empty(ISyntacticScalarConstantParser parser) => IdenticalToExpected(parser, await ScalarConstantTestData.StringValue_Empty);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticScalarConstantParser parser, ITestData<ISyntacticScalarConstant> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Value, actual.Value);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.UnitInstance, actual.Syntax.UnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.Value, actual.Syntax.Value);
    }
}
