namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, dictating the result from subtraction of two instances of the implementing quantity.</summary>
/// <typeparam name="TDifference">The quantity that represents the result from subtraction of two instances of the implementing quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityDifferenceAttribute<TDifference> : Attribute { }
