namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as an auto-generated SharpMeasures vector quantity, and a member of a group of vectors representing the same quantity.</summary>
/// <typeparam name="TGroup">The vector group that the member belongs to.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class VectorGroupMemberAttribute<TGroup> : Attribute
{
    /// <summary>The dimension of the vector space. Can be ignored if the name of the type ends with the dimension.</summary>
    public int Dimension { get; init; }

    /// <summary>Declares the marked type as an auto-generated SharpMeasures vector quantity, and a member of a group of vectors representing the same quantity.</summary>
    public VectorGroupMemberAttribute() { }
}
