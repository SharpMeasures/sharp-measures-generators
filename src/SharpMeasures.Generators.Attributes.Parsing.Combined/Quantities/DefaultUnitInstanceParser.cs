namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="IDefaultUnitInstanceParser"/>
public sealed class DefaultUnitInstanceParser : AParser<IDefaultUnitInstanceRecord>, IDefaultUnitInstanceParser
{
    /// <summary>Instantiates a <see cref="DefaultUnitInstanceParser"/>, parsing the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public DefaultUnitInstanceParser(ICombinedParser parser, IDefaultUnitInstanceRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
