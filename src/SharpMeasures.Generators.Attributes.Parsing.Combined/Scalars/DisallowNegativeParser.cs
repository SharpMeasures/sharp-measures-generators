namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="IDisallowNegativeParser"/>
public sealed class DisallowNegativeParser : AParser<IDisallowNegativeRecord>, IDisallowNegativeParser
{
    /// <summary>Instantiates a <see cref="DisallowNegativeParser"/>, parsing the arguments of <see cref="DisallowNegativeAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public DisallowNegativeParser(ICombinedParser parser, IDisallowNegativeRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
