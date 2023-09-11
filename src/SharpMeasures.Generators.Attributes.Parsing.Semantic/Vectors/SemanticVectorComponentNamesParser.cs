namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticVectorComponentNamesParser"/>
public sealed class SemanticVectorComponentNamesParser : ASemanticParser<ISemanticVectorComponentNamesRecord>, ISemanticVectorComponentNamesParser
{
    /// <summary>Instantiates a <see cref="SemanticVectorComponentNamesParser"/>, parsing the arguments of <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticVectorComponentNamesParser(ISemanticParser parser, ISemanticVectorComponentNamesRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
