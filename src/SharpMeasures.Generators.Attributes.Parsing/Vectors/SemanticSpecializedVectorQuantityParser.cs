namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticSpecializedVectorQuantityParser"/>
public sealed class SemanticSpecializedVectorQuantityParser : ASemanticParser<ISemanticSpecializedVectorQuantityRecord>, ISemanticSpecializedVectorQuantityParser
{
    /// <summary>Instantiates a <see cref="SemanticSpecializedVectorQuantityParser"/>, parsing the arguments of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticSpecializedVectorQuantityParser(ISemanticParser parser, ISemanticSpecializedVectorQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
