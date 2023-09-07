namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="ISemanticQuantityConversionParser"/>
public sealed class SemanticQuantityConversionParser : ASemanticParser<ISemanticQuantityConversionRecord>, ISemanticQuantityConversionParser
{
    /// <summary>Instantiates a <see cref="SemanticQuantityConversionParser"/>, parsing the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticQuantityConversionParser(ISemanticParser parser, ISemanticQuantityConversionRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
