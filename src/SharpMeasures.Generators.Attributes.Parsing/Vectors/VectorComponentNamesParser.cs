namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="IVectorComponentNamesParser"/>
public sealed class VectorComponentNamesParser : AParser<IVectorComponentNamesRecord>, IVectorComponentNamesParser
{
    /// <summary>Instantiates a <see cref="VectorComponentNamesParser"/>, parsing the arguments of <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public VectorComponentNamesParser(ICombinedParser parser, IVectorComponentNamesRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
