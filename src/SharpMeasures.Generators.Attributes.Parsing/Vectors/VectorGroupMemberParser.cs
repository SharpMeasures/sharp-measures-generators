namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="IVectorGroupMemberParser"/>
public sealed class VectorGroupMemberParser : AParser<IVectorGroupMemberRecord>, IVectorGroupMemberParser
{
    /// <summary>Instantiates a <see cref="VectorGroupMemberParser"/>, parsing the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public VectorGroupMemberParser(ICombinedParser parser, IVectorGroupMemberRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
