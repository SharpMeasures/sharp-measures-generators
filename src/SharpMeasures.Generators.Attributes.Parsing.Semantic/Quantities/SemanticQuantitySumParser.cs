namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="ISemanticQuantitySumParser"/>
public sealed class SemanticQuantitySumParser : ASemanticParser<ISemanticQuantitySumRecord>, ISemanticQuantitySumParser
{
    /// <summary>Instantiates a <see cref="SemanticQuantitySumParser"/>, parsing the arguments of <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticQuantitySumParser(ISemanticParser parser, ISemanticQuantitySumRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
