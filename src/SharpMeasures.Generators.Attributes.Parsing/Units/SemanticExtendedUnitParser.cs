namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="ISemanticExtendedUnitParser"/>
public sealed class SemanticExtendedUnitParser : ASemanticParser<ISemanticExtendedUnitRecord>, ISemanticExtendedUnitParser
{
    /// <summary>Instantiates a <see cref="SemanticExtendedUnitParser"/>, parsing the arguments of <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticExtendedUnitParser(ISemanticParser parser, ISemanticExtendedUnitRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
