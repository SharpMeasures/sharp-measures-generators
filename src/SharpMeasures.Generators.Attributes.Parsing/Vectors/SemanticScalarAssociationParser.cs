namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <inheritdoc cref="ISemanticScalarAssociationParser"/>
public sealed class SemanticScalarAssociationParser : ASemanticParser<ISemanticScalarAssociationRecord>, ISemanticScalarAssociationParser
{
    /// <summary>Instantiates a <see cref="SemanticScalarAssociationParser"/>, parsing the arguments of <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticScalarAssociationParser(ISemanticParser parser, ISemanticScalarAssociationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
