namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityOperationCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IQuantityOperation? Target(ISemanticQuantityOperationParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticQuantityOperationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type_Type_OperatorType(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Constructor_Type_Type_OperatorType);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OperatorType_Unrecognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.OperatorType_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task OperatorType_Recognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.OperatorType_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Position_Unrecognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Position_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Position_Recognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Position_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirrorMode_Unrecognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirrorMode_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirrorMode_Recognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirrorMode_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Implementation_Unrecognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Implementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Implementation_Recognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.Implementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredImplementation_Unrecognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredImplementation_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredImplementation_Recognized(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredImplementation_Recognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Null(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Empty(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_String(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_Null(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_Empty(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task StaticMethodName_String(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.StaticMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_Null(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_Empty(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredMethodName_String(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredMethodName_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_Null(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_Empty(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MirroredStaticMethodName_String(ISemanticQuantityOperationParser parser) => IdenticalToExpected(parser, await QuantityOperationTestData.MirroredStaticMethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticQuantityOperationParser parser, ITestData<IQuantityOperation> data)
    {
        var actual = Target(parser, data.AttributeData);

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
    }
}
