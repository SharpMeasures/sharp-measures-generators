namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface IScalarAssociation
{
    /// <summary>The scalar quantity associated with the vector quantity.</summary>
    public abstract ITypeSymbol ScalarQuantity { get; }

    /// <summary>Dictates whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    public abstract bool? AsComponents { get; }

    /// <summary>Dictates whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</summary>
    public abstract bool? AsMagnitude { get; }
}
