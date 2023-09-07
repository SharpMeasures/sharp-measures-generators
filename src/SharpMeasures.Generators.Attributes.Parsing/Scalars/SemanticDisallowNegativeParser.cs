namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISemanticDisallowNegativeParser"/>
public sealed class SemanticDisallowNegativeParser : ASemanticParser<ISemanticDisallowNegativeRecord>, ISemanticDisallowNegativeParser
{
    /// <summary>Instantiates a <see cref="SemanticDisallowNegativeParser"/>, parsing the arguments of <see cref="DisallowNegativeAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticDisallowNegativeParser(ISemanticParser parser, ISemanticDisallowNegativeRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
