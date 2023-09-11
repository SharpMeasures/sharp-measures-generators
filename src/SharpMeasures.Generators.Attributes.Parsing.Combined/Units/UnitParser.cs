namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="IUnitParser"/>
public sealed class UnitParser : AParser<IUnitRecord>, IUnitParser
{
    /// <summary>Instantiates a <see cref="UnitParser"/>, parsing the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public UnitParser(ICombinedParser parser, IUnitRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
