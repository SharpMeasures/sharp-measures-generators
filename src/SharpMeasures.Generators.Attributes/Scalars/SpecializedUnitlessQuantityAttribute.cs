namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures unit-less quantity, behaving as a specialized form of another quantity, <typeparamref name="TOriginal"/>.</summary>
/// <typeparam name="TOriginal">The original quantity, of which this quantity is a specialized form.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class SpecializedUnitlessQuantityAttribute<TOriginal> : Attribute
{
    /// <inheritdoc cref="SpecializedUnitlessQuantityAttribute{TQuantity}"/>
    public SpecializedUnitlessQuantityAttribute() { }
}
