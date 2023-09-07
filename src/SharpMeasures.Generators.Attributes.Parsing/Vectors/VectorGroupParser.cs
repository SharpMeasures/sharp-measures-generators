namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="IVectorGroupParser"/>
public sealed class VectorGroupParser : AParser<IVectorGroupRecord>, IVectorGroupParser
{
    /// <summary>Instantiates a <see cref="VectorGroupParser"/>, parsing the arguments of <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public VectorGroupParser(ICombinedParser parser, IVectorGroupRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
