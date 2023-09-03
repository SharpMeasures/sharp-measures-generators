namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface ISemanticScalarAssociationRecorderFactory : ISemanticRecorderFactory<ISemanticScalarAssociationRecord> { }
