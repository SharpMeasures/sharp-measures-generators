namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as an auto-generated SharpMeasures scalar quantity, behaving as a specialized form of another quantity.</summary>
/// <typeparam name="TOriginal">The original quantity, of which this quantity is a specialized form.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class SpecializedScalarQuantityAttribute<TOriginal> : Attribute
{
    /// <summary>Declares the marked type as an auto-generated SharpMeasures scalar quantity, behaving as a specialized form of another quantity.</summary>
    public SpecializedScalarQuantityAttribute() { }
}
