namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="INegativeMagnitudeBehaviourParser"/>
public sealed class NegativeMagnitudeBehaviourParser : AParser<INegativeMagnitudeBehaviourRecord>, INegativeMagnitudeBehaviourParser
{
    /// <summary>Instantiates a <see cref="NegativeMagnitudeBehaviourParser"/>, parsing the arguments of <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public NegativeMagnitudeBehaviourParser(ICombinedParser parser, INegativeMagnitudeBehaviourRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
