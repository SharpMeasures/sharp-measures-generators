namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as a auto-generated SharpMeasures vector quantity, and a member of a group of vectors representing the same quantity.</summary>
/// <typeparam name="TGroup">The vector group that the member belongs to.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorGroupMemberAttribute<TGroup> : Attribute
{
    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}.Dimension"/>
    public int Dimension { get; init; }

    /// <inheritdoc cref="VectorGroupMemberAttribute{TGroup}"/>
    public VectorGroupMemberAttribute() { }
}
