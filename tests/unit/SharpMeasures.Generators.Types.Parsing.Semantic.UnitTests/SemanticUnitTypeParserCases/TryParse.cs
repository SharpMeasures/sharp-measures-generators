namespace SharpMeasures.Generators.Types.Parsing.SemanticUnitTypeParserCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Types.Units;

using System;

using Xunit;

public sealed class TryParse
{
    private static ISemanticUnitType? Target(ISemanticUnitTypeParser parser, ITypeSymbol type) => parser.TryParse(type);

    private ParserContext Context { get; } = ParserContext.Create();

    [Fact]
    public void NullType_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Parser, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }
}
