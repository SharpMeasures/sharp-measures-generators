namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="IVectorQuantityParser"/>
public sealed class VectorQuantityParser : AParser<IVectorQuantityRecord>, IVectorQuantityParser
{
    /// <summary>Instantiates a <see cref="VectorQuantityParser"/>, parsing the arguments of <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public VectorQuantityParser(ICombinedParser parser, IVectorQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
