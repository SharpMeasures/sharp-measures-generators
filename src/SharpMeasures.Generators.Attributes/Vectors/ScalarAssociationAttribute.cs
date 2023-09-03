namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures vector quantities, describing the marked quantity as associated with a scalar quantity.</summary>
/// <typeparam name="TScalar">The scalar quantity associated with the vector quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class ScalarAssociationAttribute<TScalar> : Attribute
{
    /// <summary>Determines whether the associated scalar quantity should be used to describe each Cartesian component of the vector quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool AsComponents { get; init; }

    /// <summary>Determines whether the associated scalar quantity should be used to describe the magnitude of the vector quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool AsMagnitude { get; init; }

    /// <summary>Describes the marked vector quantity as associated with a scalar quantity.</summary>
    public ScalarAssociationAttribute() { }
}
