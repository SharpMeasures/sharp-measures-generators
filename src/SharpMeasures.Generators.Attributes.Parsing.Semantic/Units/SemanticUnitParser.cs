namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="ISemanticUnitParser"/>
public sealed class SemanticUnitParser : ASemanticParser<ISemanticUnitRecord>, ISemanticUnitParser
{
    /// <summary>Instantiates a <see cref="SemanticUnitParser"/>, parsing the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticUnitParser(ISemanticParser parser, ISemanticUnitRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
