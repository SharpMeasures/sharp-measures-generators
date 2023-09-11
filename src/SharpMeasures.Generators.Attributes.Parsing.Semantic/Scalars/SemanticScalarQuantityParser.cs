namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISemanticScalarQuantityParser"/>
public sealed class SemanticScalarQuantityParser : ASemanticParser<ISemanticScalarQuantityRecord>, ISemanticScalarQuantityParser
{
    /// <summary>Instantiates a <see cref="SemanticScalarQuantityParser"/>, parsing the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticScalarQuantityParser(ISemanticParser parser, ISemanticScalarQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
