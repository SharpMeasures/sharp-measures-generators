namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface IVectorAssociation
{
    /// <summary>The vector quantity associated with the scalar quantity.</summary>
    public abstract ITypeSymbol VectorQuantity { get; }
}
