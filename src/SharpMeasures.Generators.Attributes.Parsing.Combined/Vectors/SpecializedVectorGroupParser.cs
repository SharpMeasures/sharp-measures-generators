namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISpecializedVectorGroupParser"/>
public sealed class SpecializedVectorGroupParser : AParser<ISpecializedVectorGroupRecord>, ISpecializedVectorGroupParser
{
    /// <summary>Instantiates a <see cref="SpecializedVectorGroupParser"/>, parsing the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SpecializedVectorGroupParser(ICombinedParser parser, ISpecializedVectorGroupRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
