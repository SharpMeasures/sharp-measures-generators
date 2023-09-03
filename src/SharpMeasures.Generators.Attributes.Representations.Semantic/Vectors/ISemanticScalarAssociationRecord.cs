namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface ISemanticScalarAssociationRecord
{
    /// <summary>The scalar quantity associated with the vector quantity.</summary>
    public abstract ITypeSymbol ScalarQuantity { get; }

    /// <summary>Indicates whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    public abstract OneOf<None, bool> AsComponents { get; }

    /// <summary>Indicates whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</summary>
    public abstract OneOf<None, bool> AsMagnitude { get; }
}
