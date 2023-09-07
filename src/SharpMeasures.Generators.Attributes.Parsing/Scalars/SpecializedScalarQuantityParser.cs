namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISpecializedScalarQuantityParser"/>
public sealed class SpecializedScalarQuantityParser : AParser<ISpecializedScalarQuantityRecord>, ISpecializedScalarQuantityParser
{
    /// <summary>Instantiates a <see cref="SpecializedScalarQuantityParser"/>, parsing the arguments of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SpecializedScalarQuantityParser(ICombinedParser parser, ISpecializedScalarQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
