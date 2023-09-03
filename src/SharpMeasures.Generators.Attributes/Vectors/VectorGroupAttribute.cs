namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but in different vector spaces.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorGroupAttribute<TUnit> : Attribute
{
    /// <summary>Declares the marked type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but in different vector spaces.</summary>
    public VectorGroupAttribute() { }
}
