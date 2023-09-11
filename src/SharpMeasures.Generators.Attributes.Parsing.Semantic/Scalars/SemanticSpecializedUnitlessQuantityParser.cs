namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISemanticSpecializedUnitlessQuantityParser"/>
public sealed class SemanticSpecializedUnitlessQuantityParser : ASemanticParser<ISemanticSpecializedUnitlessQuantityRecord>, ISemanticSpecializedUnitlessQuantityParser
{
    /// <summary>Instantiates a <see cref="SemanticSpecializedUnitlessQuantityParser"/>, parsing the arguments of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticSpecializedUnitlessQuantityParser(ISemanticParser parser, ISemanticSpecializedUnitlessQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
