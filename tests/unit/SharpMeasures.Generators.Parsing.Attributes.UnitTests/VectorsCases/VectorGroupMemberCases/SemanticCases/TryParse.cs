namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorGroupMemberCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IVectorGroupMember? Target(ISemanticVectorGroupMemberParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticVectorGroupMemberParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Type(ISemanticVectorGroupMemberParser parser) => IdenticalToExpected(parser, await VectorGroupMemberTestData.Constructor_Type);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Dimension(ISemanticVectorGroupMemberParser parser) => IdenticalToExpected(parser, await VectorGroupMemberTestData.Dimension);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticVectorGroupMemberParser parser, ITestData<IVectorGroupMember> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Group, actual.Group, ReferenceTypeSymbolComparer.IndividualComparer);
        Assert.Equal(data.ExpectedResult.Dimension, actual.Dimension);
    }
}
