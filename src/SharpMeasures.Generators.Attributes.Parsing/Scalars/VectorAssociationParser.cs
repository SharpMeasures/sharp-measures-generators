namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <inheritdoc cref="IVectorAssociationParser"/>
public sealed class VectorAssociationParser : AParser<IVectorAssociationRecord>, IVectorAssociationParser
{
    /// <summary>Instantiates a <see cref="VectorAssociationParser"/>, parsing the arguments of <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public VectorAssociationParser(ICombinedParser parser, IVectorAssociationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
