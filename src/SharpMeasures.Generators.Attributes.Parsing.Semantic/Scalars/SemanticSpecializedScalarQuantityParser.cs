namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISemanticSpecializedScalarQuantityParser"/>
public sealed class SemanticSpecializedScalarQuantityParser : ASemanticParser<ISemanticSpecializedScalarQuantityRecord>, ISemanticSpecializedScalarQuantityParser
{
    /// <summary>Instantiates a <see cref="SemanticSpecializedScalarQuantityParser"/>, parsing the arguments of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticSpecializedScalarQuantityParser(ISemanticParser parser, ISemanticSpecializedScalarQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
