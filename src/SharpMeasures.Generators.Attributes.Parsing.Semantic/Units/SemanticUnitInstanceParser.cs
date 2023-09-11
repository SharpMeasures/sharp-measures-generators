namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="ISemanticUnitInstanceParser"/>
public sealed class SemanticUnitInstanceParser : ASemanticParser<ISemanticUnitInstanceRecord>, ISemanticUnitInstanceParser
{
    /// <summary>Instantiates a <see cref="SemanticUnitInstanceParser"/>, parsing the arguments of <see cref="UnitInstanceAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticUnitInstanceParser(ISemanticParser parser, ISemanticUnitInstanceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
