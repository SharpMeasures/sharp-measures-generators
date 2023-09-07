namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as an auto-generated SharpMeasures unit, behaving as an extension of another unit.</summary>
/// <typeparam name="TOriginal">The original quantity, of which this quantity is a specialized form.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class ExtendedUnitAttribute<TOriginal> : Attribute
{
    /// <summary>Declares the marked type as an auto-generated SharpMeasures unit, behaving as an extension of another unit.</summary>
    public ExtendedUnitAttribute() { }
}
