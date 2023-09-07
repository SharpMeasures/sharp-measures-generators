namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="IQuantityConversionParser"/>
public sealed class QuantityConversionParser : AParser<IQuantityConversionRecord>, IQuantityConversionParser
{
    /// <summary>Instantiates a <see cref="QuantityConversionParser"/>, parsing the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public QuantityConversionParser(ICombinedParser parser, IQuantityConversionRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
