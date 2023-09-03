namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface IScalarAssociationRecord : ISemanticScalarAssociationRecord
{
    /// <summary>Represents syntactic information about the <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    public abstract ISyntacticScalarAssociationRecord Syntactic { get; }
}
