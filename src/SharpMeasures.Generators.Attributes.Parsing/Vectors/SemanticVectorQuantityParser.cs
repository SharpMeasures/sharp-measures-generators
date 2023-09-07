namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticVectorQuantityParser"/>
public sealed class SemanticVectorQuantityParser : ASemanticParser<ISemanticVectorQuantityRecord>, ISemanticVectorQuantityParser
{
    /// <summary>Instantiates a <see cref="SemanticVectorQuantityParser"/>, parsing the arguments of <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticVectorQuantityParser(ISemanticParser parser, ISemanticVectorQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
