namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISpecializedVectorQuantityParser"/>
public sealed class SpecializedVectorQuantityParser : AParser<ISpecializedVectorQuantityRecord>, ISpecializedVectorQuantityParser
{
    /// <summary>Instantiates a <see cref="SpecializedVectorQuantityParser"/>, parsing the arguments of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SpecializedVectorQuantityParser(ICombinedParser parser, ISpecializedVectorQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
