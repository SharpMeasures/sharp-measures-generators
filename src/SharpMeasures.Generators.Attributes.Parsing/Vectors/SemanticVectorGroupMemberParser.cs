namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticVectorGroupMemberParser"/>
public sealed class SemanticVectorGroupMemberParser : ASemanticParser<ISemanticVectorGroupMemberRecord>, ISemanticVectorGroupMemberParser
{
    /// <summary>Instantiates a <see cref="SemanticVectorGroupMemberParser"/>, parsing the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticVectorGroupMemberParser(ISemanticParser parser, ISemanticVectorGroupMemberRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
