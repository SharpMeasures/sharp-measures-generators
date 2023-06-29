namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.ScalarAssociationCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IScalarAssociation? Target(ISemanticScalarAssociationParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticScalarAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsComponents_True(ISemanticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsComponents_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsComponents_False(ISemanticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsComponents_False);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsMagnitude_True(ISemanticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsMagnitude_True);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task AsMagnitude_False(ISemanticScalarAssociationParser parser) => IdenticalToExpected(parser, await ScalarAssociationTestData.AsMagnitude_False);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticScalarAssociationParser parser, ITestData<IScalarAssociation> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.ScalarQuantity, actual.ScalarQuantity, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.AsComponents, actual.AsComponents);
        Assert.Equal(data.ExpectedResult.AsMagnitude, actual.AsMagnitude);
    }
}
