namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticNegativeMagnitudeBehaviourParser"/>
public sealed class SemanticNegativeMagnitudeBehaviourParser : ASemanticParser<ISemanticNegativeMagnitudeBehaviourRecord>, ISemanticNegativeMagnitudeBehaviourParser
{
    /// <summary>Instantiates a <see cref="SemanticNegativeMagnitudeBehaviourParser"/>, parsing the arguments of <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticNegativeMagnitudeBehaviourParser(ISemanticParser parser, ISemanticNegativeMagnitudeBehaviourRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
