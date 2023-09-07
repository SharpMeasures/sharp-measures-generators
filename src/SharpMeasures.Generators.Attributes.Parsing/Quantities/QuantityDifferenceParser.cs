namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="IQuantityDifferenceParser"/>
public sealed class QuantityDifferenceParser : AParser<IQuantityDifferenceRecord>, IQuantityDifferenceParser
{
    /// <summary>Instantiates a <see cref="QuantityDifferenceParser"/>, parsing the arguments of <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public QuantityDifferenceParser(ICombinedParser parser, IQuantityDifferenceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
