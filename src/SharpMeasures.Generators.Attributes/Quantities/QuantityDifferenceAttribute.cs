namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, specifying the result from subtraction of two instances of the marked quantity.</summary>
/// <typeparam name="TDifference">The quantity that represents the result from subtraction of two instances of the marked quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityDifferenceAttribute<TDifference> : Attribute
{
    /// <summary>Specifies the result from subtraction of two instances of the marked quantity.</summary>
    public QuantityDifferenceAttribute() { }
}
