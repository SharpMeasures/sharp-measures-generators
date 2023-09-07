namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISpecializedUnitlessQuantityParser"/>
public sealed class SpecializedUnitlessQuantityParser : AParser<ISpecializedUnitlessQuantityRecord>, ISpecializedUnitlessQuantityParser
{
    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantityParser"/>, parsing the arguments of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SpecializedUnitlessQuantityParser(ICombinedParser parser, ISpecializedUnitlessQuantityRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
