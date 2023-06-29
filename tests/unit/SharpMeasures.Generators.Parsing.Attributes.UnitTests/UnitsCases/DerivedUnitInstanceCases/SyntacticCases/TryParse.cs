namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.DerivedUnitInstanceCases.SyntacticCases;

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
    private static ISyntacticDerivedUnitInstance? Target(ISyntacticDerivedUnitInstanceParser parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISyntacticDerivedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!, AttributeSyntaxFactory.Create()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeSyntax_ArgumentNullException(ISyntacticDerivedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, Mock.Of<AttributeData>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_StringCollection(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_StringCollection(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_StringCollection(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Null(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Empty(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_String(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Null(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Empty(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Populated(ISyntacticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Populated);

    [AssertionMethod]
    private static void IdenticalToExpected(ISyntacticDerivedUnitInstanceParser parser, ITestData<ISyntacticDerivedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData, data.AttributeSyntax);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.DerivationID, actual.DerivationID);
        Assert.Equal(data.ExpectedResult.UnitInstances, actual.UnitInstances);

        Assert.Equal(data.ExpectedResult.Syntax.AttributeName, actual.Syntax.AttributeName);
        Assert.Equal(data.ExpectedResult.Syntax.Attribute, actual.Syntax.Attribute);
        Assert.Equal(data.ExpectedResult.Syntax.Name, actual.Syntax.Name);
        Assert.Equal(data.ExpectedResult.Syntax.PluralForm, actual.Syntax.PluralForm);
        Assert.Equal(data.ExpectedResult.Syntax.DerivationID, actual.Syntax.DerivationID);
        Assert.Equal(data.ExpectedResult.Syntax.UnitInstancesCollection, actual.Syntax.UnitInstancesCollection);
        Assert.Equal(data.ExpectedResult.Syntax.UnitInstancesElements, actual.Syntax.UnitInstancesElements);
    }
}
