namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <inheritdoc cref="IExtendedUnitParser"/>
public sealed class ExtendedUnitParser : AParser<IExtendedUnitRecord>, IExtendedUnitParser
{
    /// <summary>Instantiates a <see cref="ExtendedUnitParser"/>, parsing the arguments of <see cref="UnitExtensionAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public ExtendedUnitParser(ICombinedParser parser, IExtendedUnitRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
