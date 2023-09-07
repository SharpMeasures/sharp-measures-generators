namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="IScalarAssociationParser"/>
public sealed class ScalarAssociationParser : AParser<IScalarAssociationRecord>, IScalarAssociationParser
{
    /// <summary>Instantiates a <see cref="ScalarAssociationParser"/>, parsing the arguments of <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public ScalarAssociationParser(ICombinedParser parser, IScalarAssociationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
