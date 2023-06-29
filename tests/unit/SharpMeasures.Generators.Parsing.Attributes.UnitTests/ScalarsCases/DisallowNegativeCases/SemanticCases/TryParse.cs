namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.DisallowNegativeCases.SemanticCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class TryParse
{
    private static IDisallowNegative? Target(ISemanticDisallowNegativeParser parser, AttributeData attributeData) => parser.TryParse(attributeData);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public void NullAttributeData_ArgumentNullException(ISemanticDisallowNegativeParser parser)
    {
        var exception = Record.Exception(() => Target(parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_Empty(ISemanticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Constructor_Empty);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Constructor_DisallowNegativeBehaviour(ISemanticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Constructor_DisallowNegativeBehaviour);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Behaviour_Unrecognized(ISemanticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Behaviour_Unrecognized);

    [Theory]
    [ClassData(typeof(ParserSources))]
    public async Task Behaviour_Recognized(ISemanticDisallowNegativeParser parser) => IdenticalToExpected(parser, await DisallowNegativeTestData.Behaviour_Recognized);

    [AssertionMethod]
    private static void IdenticalToExpected(ISemanticDisallowNegativeParser parser, ITestData<IDisallowNegative> data)
    {
        var actual = Target(parser, data.AttributeData);

        Assert.NotNull(actual);

        Assert.Equal(data.ExpectedResult.Behaviour, actual.Behaviour);
    }
}
