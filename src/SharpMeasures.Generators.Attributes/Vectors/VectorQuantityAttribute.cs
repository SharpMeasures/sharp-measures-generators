namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures vector quantity.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorQuantityAttribute<TUnit> : Attribute
{
    /// <summary>The dimension of the quantity.</summary>
    /// <remarks>This does not have to be explicitly specified if the name of the type ends with the dimension - for example, { <i>Position3</i> }.</remarks>
    public int Dimension { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}"/>
    public VectorQuantityAttribute() { }
}
