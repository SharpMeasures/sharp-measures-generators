namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityOperationCases.SyntacticCases;

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
    private static ISyntacticQuantityOperation? Target(ISyntacticQuantityOperationParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticQuantityOperationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticQuantityOperationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_Type_OperatorType(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Constructor_Type_Type_OperatorType);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OperatorType_Unrecognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.OperatorType_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OperatorType_Recognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.OperatorType_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Position_Unrecognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Position_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Position_Recognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Position_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirrorMode_Unrecognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirrorMode_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirrorMode_Recognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirrorMode_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Implementation_Unrecognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Implementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Implementation_Recognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Implementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredImplementation_Unrecognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredImplementation_Recognized(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Null(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Empty(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_String(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_Null(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_Empty(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_String(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_Null(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_Empty(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_String(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_Null(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_Empty(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_String(ISyntacticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticQuantityOperationParser parser, ITestData<ISyntacticQuantityOperation> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Result, actual.Result, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Other, actual.Other, ReferenceTypeSymbolComparer.IndividualComparer);

        Assert.Equal(data.ExpectedResult.OperatorType, actual.OperatorType);
        Assert.Equal(data.ExpectedResult.Position, actual.Position);
        Assert.Equal(data.ExpectedResult.MirrorMode, actual.MirrorMode);
        Assert.Equal(data.ExpectedResult.Implementation, actual.Implementation);
        Assert.Equal(data.ExpectedResult.MirroredImplementation, actual.MirroredImplementation);

        Assert.Equal(data.ExpectedResult.MethodName, actual.MethodName);
        Assert.Equal(data.ExpectedResult.StaticMethodName, actual.StaticMethodName);
        Assert.Equal(data.ExpectedResult.MirroredMethodName, actual.MirroredMethodName);
        Assert.Equal(data.ExpectedResult.MirroredStaticMethodName, actual.MirroredStaticMethodName);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);

        Assert.Equal(data.ExpectedResult.Syntax.Result, actual.Syntax.Result);
        Assert.Equal(data.ExpectedResult.Syntax.Other, actual.Syntax.Other);

        Assert.Equal(data.ExpectedResult.Syntax.OperatorType, actual.Syntax.OperatorType);
        Assert.Equal(data.ExpectedResult.Syntax.Position, actual.Syntax.Position);
        Assert.Equal(data.ExpectedResult.Syntax.MirrorMode, actual.Syntax.MirrorMode);
        Assert.Equal(data.ExpectedResult.Syntax.Implementation, actual.Syntax.Implementation);
        Assert.Equal(data.ExpectedResult.Syntax.MirroredImplementation, actual.Syntax.MirroredImplementation);

        Assert.Equal(data.ExpectedResult.Syntax.MethodName, actual.Syntax.MethodName);
        Assert.Equal(data.ExpectedResult.Syntax.StaticMethodName, actual.Syntax.StaticMethodName);
        Assert.Equal(data.ExpectedResult.Syntax.MirroredMethodName, actual.Syntax.MirroredMethodName);
        Assert.Equal(data.ExpectedResult.Syntax.MirroredStaticMethodName, actual.Syntax.MirroredStaticMethodName);
    }
}
