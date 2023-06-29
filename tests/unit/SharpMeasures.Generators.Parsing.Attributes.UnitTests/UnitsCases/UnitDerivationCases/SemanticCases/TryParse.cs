namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitDerivationCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IUnitDerivation? Target(ISemanticUnitDerivationParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticUnitDerivationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_TypeCollection(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Constructor_String_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_TypeCollection(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Constructor_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Null(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Empty(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_String(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Null(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Empty(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Populated(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Null(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Empty(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_String(ISemanticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticUnitDerivationParser parser, ITestData<IUnitDerivation> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.DerivationID, actual.DerivationID);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
        Assert.Equal(data.ExpectedResult.Signature, actual.Signature, ReferenceTypeSymbolComparer.CollectionComparer);
        Assert.Equal(data.ExpectedResult.MethodName, actual.MethodName);
    }
}
