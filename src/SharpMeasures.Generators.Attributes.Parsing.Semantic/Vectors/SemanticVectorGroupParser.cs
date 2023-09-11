namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticVectorGroupParser"/>
public sealed class SemanticVectorGroupParser : ASemanticParser<ISemanticVectorGroupRecord>, ISemanticVectorGroupParser
{
    /// <summary>Instantiates a <see cref="SemanticVectorGroupParser"/>, parsing the arguments of <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticVectorGroupParser(ISemanticParser parser, ISemanticVectorGroupRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
