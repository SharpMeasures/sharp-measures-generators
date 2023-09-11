namespace SharpMeasures.Generators.Types.Parsing.SemanticUnitTypeParserCases;

using Moq;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal sealed class ParserContext
{
    public static ParserContext Create()
    {
        Mock<ISemanticUnitParser> unitParserMock = new();
        Mock<ISemanticUnitInstanceParser> unitInstanceParserMock = new();

        SemanticUnitTypeParser parser = new(unitParserMock.Object, unitInstanceParserMock.Object);

        return new(parser, unitParserMock, unitInstanceParserMock);
    }

    public SemanticUnitTypeParser Parser { get; }

    public Mock<ISemanticUnitParser> UnitParserMock { get; }
    public Mock<ISemanticUnitInstanceParser> UnitInstanceParserMock { get; }

    private ParserContext(SemanticUnitTypeParser parser, Mock<ISemanticUnitParser> unitParserMock, Mock<ISemanticUnitInstanceParser> unitInstanceParserMock)
    {
        Parser = parser;

        UnitParserMock = unitParserMock;
        UnitInstanceParserMock = unitInstanceParserMock;
    }
}
