namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but in different vector spaces - behaving as a specialized form of another such group.</summary>
/// <typeparam name="TOriginal">The original group, of which this group is a specialized form.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class SpecializedVectorGroupAttribute<TOriginal> : Attribute
{
    /// <summary>Declares the marked type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but in different vector spaces - behaving as a specialized form of another such group.</summary>
    public SpecializedVectorGroupAttribute() { }
}
