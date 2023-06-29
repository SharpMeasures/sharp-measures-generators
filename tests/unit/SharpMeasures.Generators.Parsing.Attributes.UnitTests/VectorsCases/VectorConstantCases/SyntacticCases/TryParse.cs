namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorConstantCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorConstantCases.SemanticCases;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticVectorConstant? Target(ISyntacticVectorConstantParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticVectorConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticVectorConstantParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_DoubleCollection(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Constructor_String_String_DoubleCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_StringCollection(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Constructor_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Null(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_Empty(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstance_String(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.UnitInstance_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Null(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Empty(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DoubleCollection_Populated(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.DoubleCollection_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Null(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Empty(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StringCollection_Populated(ISyntacticVectorConstantParser parser) => IdenticalToExpected(parser, await VectorConstantTestData.StringCollection_Populated);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticVectorConstantParser parser, ITestData<ISyntacticVectorConstant> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.UnitInstance, actual.UnitInstance);
        Assert.Equal(data.ExpectedResult.Value, actual.Value, ValueEqualityComparer.Comparer);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.UnitInstance, actual.Syntax.UnitInstance);
        Assert.Equal(data.ExpectedResult.Syntax.ValueCollection, actual.Syntax.ValueCollection);
        Assert.Equal(data.ExpectedResult.Syntax.ValueElements, actual.Syntax.ValueElements);
    }
}
