namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="ISemanticVectorAssociationParser"/>
public sealed class SemanticVectorAssociationParser : ASemanticParser<ISemanticVectorAssociationRecord>, ISemanticVectorAssociationParser
{
    /// <summary>Instantiates a <see cref="SemanticVectorAssociationParser"/>, parsing the arguments of <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticVectorAssociationParser(ISemanticParser parser, ISemanticVectorAssociationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
