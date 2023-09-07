namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpAttributeParser;

/// <inheritdoc cref="ISemanticTypeConversionParser"/>
public sealed class SemanticTypeConversionParser : ASemanticParser<ISemanticTypeConversionRecord>, ISemanticTypeConversionParser
{
    /// <summary>Instantiates a <see cref="SemanticTypeConversionParser"/>, parsing the arguments of <see cref="TypeConversionAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticTypeConversionParser(ISemanticParser parser, ISemanticTypeConversionRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
