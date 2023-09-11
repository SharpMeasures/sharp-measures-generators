namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="IScalarQuantityParser"/>
public sealed class ScalarQuantityParser : AParser<IScalarQuantityRecord>, IScalarQuantityParser
{
    /// <summary>Instantiates a <see cref="ScalarQuantityParser"/>, parsing the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public ScalarQuantityParser(ICombinedParser parser, IScalarQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
