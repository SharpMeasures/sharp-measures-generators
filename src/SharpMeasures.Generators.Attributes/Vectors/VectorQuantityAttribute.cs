namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures vector quantity.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorQuantityAttribute<TUnit> : Attribute
{
    /// <summary>The dimension of the vector space. Can be ignored if the name of the type ends with the dimension.</summary>
    public int Dimension { get; init; }

    /// <summary>Declares the marked type as an auto-generated SharpMeasures vector quantity.</summary>
    public VectorQuantityAttribute() { }
}
