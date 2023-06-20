namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but of varying dimension - behaving as a specialized form of another such group.</summary>
/// <typeparam name="TOriginal">The original group, of which this group is a specialized form.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class SpecializedVectorGroupAttribute<TOriginal> : Attribute
{
    /// <inheritdoc cref="SpecializedVectorGroupAttribute{TOriginal}"/>
    public SpecializedVectorGroupAttribute() { }
}
