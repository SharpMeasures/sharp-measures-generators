namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="IUnitInstanceParser"/>
public sealed class UnitInstanceParser : AParser<IUnitInstanceRecord>, IUnitInstanceParser
{
    /// <summary>Instantiates a <see cref="UnitInstanceParser"/>, parsing the arguments of <see cref="UnitInstanceAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public UnitInstanceParser(ICombinedParser parser, IUnitInstanceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
