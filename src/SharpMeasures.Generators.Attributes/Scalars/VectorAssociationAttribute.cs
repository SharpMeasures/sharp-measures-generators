namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures scalar quantities, declaring the marked quantity as associated with a vector quantity - allowing multiplication of the scalar quantity by pure vectors, resulting in the specified vector quantity.</summary>
/// <typeparam name="TVector">The vector quantity associated with the scalar quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorAssociationAttribute<TVector> : Attribute
{
    /// <summary>Declares the marked scalar quantity as associated with a vector quantity.</summary>
    public VectorAssociationAttribute() { }
}
