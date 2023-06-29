namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.VectorAssociationCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IVectorAssociation? Target(ISemanticVectorAssociationParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticVectorAssociationParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticVectorAssociationParser parser) => IdenticalToExpected(parser, await VectorAssociationTestData.Constructor_Type);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticVectorAssociationParser parser, ITestData<IVectorAssociation> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.VectorQuantity, actual.VectorQuantity, ReferenceTypeSymbolComparer.IndividualComparer);
    }
}
