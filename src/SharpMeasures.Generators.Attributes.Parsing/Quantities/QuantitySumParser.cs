namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="IQuantitySumParser"/>
public sealed class QuantitySumParser : AParser<IQuantitySumRecord>, IQuantitySumParser
{
    /// <summary>Instantiates a <see cref="QuantitySumParser"/>, parsing the arguments of <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public QuantitySumParser(ICombinedParser parser, IQuantitySumRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
