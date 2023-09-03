namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface IVectorAssociationRecord : ISemanticVectorAssociationRecord
{
    /// <summary>Represents syntactic information about the <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    public abstract ISyntacticVectorAssociationRecord Syntactic { get; }
}
