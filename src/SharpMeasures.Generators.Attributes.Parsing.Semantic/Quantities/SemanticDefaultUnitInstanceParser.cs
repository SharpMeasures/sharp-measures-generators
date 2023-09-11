namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="ISemanticDefaultUnitInstanceParser"/>
public sealed class SemanticDefaultUnitInstanceParser : ASemanticParser<ISemanticDefaultUnitInstanceRecord>, ISemanticDefaultUnitInstanceParser
{
    /// <summary>Instantiates a <see cref="SemanticDefaultUnitInstanceParser"/>, parsing the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticDefaultUnitInstanceParser(ISemanticParser parser, ISemanticDefaultUnitInstanceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
