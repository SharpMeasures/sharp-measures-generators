namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitDerivationCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static ISyntacticUnitDerivation? Target(ISyntacticUnitDerivationParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticUnitDerivationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticUnitDerivationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_TypeCollection(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Constructor_String_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_TypeCollection(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Constructor_String_TypeCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Null(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Empty(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_String(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.DerivationID_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Null(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_Empty(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Expression_String(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Expression_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Null(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Empty(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Signature_Populated(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.Signature_Populated);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Null(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_Empty(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task MethodName_String(ISyntacticUnitDerivationParser parser) => IdenticalToExpected(parser, await UnitDerivationTestData.MethodName_String);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticUnitDerivationParser parser, ITestData<ISyntacticUnitDerivation> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.DerivationID, actual.DerivationID);
        Assert.Equal(data.ExpectedResult.Expression, actual.Expression);
        Assert.Equal(data.ExpectedResult.Signature, actual.Signature, ReferenceTypeSymbolComparer.CollectionComparer);
        Assert.Equal(data.ExpectedResult.MethodName, actual.MethodName);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.DerivationID, actual.Syntax.DerivationID);
        Assert.Equal(data.ExpectedResult.Syntax.Expression, actual.Syntax.Expression);
        Assert.Equal(data.ExpectedResult.Syntax.SignatureCollection, actual.Syntax.SignatureCollection);
        Assert.Equal(data.ExpectedResult.Syntax.SignatureElements, actual.Syntax.SignatureElements);
        Assert.Equal(data.ExpectedResult.Syntax.MethodName, actual.Syntax.MethodName);
    }
}
