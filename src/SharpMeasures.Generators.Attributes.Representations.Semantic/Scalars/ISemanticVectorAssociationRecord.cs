namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface ISemanticVectorAssociationRecord
{
    /// <summary>The vector quantity associated with the scalar quantity.</summary>
    public abstract ITypeSymbol VectorQuantity { get; }
}
