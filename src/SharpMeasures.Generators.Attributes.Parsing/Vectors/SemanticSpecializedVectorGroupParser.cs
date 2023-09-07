namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticSpecializedVectorGroupParser"/>
public sealed class SemanticSpecializedVectorGroupParser : ASemanticParser<ISemanticSpecializedVectorGroupRecord>, ISemanticSpecializedVectorGroupParser
{
    /// <summary>Instantiates a <see cref="SemanticSpecializedVectorGroupParser"/>, parsing the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticSpecializedVectorGroupParser(ISemanticParser parser, ISemanticSpecializedVectorGroupRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
