namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.DerivedUnitInstanceCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IDerivedUnitInstance? Target(ISemanticDerivedUnitInstanceParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticDerivedUnitInstanceParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_String_StringCollection(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_String_StringCollection(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_String_StringCollection(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Constructor_String_StringCollection);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Null(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_Empty(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Name_String(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.Name_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Null(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_Empty(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task PluralForm_String(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.PluralForm_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Null(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_Empty(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task DerivationID_String(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.DerivationID_String);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Null(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Null);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Empty(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task UnitInstances_Populated(ISemanticDerivedUnitInstanceParser parser) => IdenticalToExpected(parser, await DerivedUnitInstanceTestData.UnitInstances_Populated);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticDerivedUnitInstanceParser parser, ITestData<IDerivedUnitInstance> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Name, actual.Name);
        Assert.Equal(data.ExpectedResult.PluralForm, actual.PluralForm);
        Assert.Equal(data.ExpectedResult.DerivationID, actual.DerivationID);
        Assert.Equal(data.ExpectedResult.UnitInstances, actual.UnitInstances);
    }
}
