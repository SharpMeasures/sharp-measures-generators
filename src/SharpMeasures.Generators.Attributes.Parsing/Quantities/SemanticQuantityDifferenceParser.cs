namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="ISemanticQuantityDifferenceParser"/>
public sealed class SemanticQuantityDifferenceParser : ASemanticParser<ISemanticQuantityDifferenceRecord>, ISemanticQuantityDifferenceParser
{
    /// <summary>Instantiates a <see cref="SemanticQuantityDifferenceParser"/>, parsing the arguments of <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticQuantityDifferenceParser(ISemanticParser parser, ISemanticQuantityDifferenceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
