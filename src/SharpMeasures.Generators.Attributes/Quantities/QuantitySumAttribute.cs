namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, specifying the result from addition of two instances of the marked quantity.</summary>
/// <typeparam name="TSum">The quantity that represents the result from addition of two instances of the marked quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantitySumAttribute<TSum> : Attribute
{
    /// <summary>Specifies the result from addition of two instances of the marked quantity.</summary>
    public QuantitySumAttribute() { }
}
