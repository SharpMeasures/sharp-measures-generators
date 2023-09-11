namespace SharpMeasures.Quantities;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, extending the set of unit instances referenced by the marked quantity.</summary>
/// <typeparam name="TProvider">The type providing the additional unit instances.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class UseUnitProviderAttribute<TProvider> : Attribute
{
    /// <summary>Extends the set of unit instances referenced by the marked quantity.</summary>
    public UseUnitProviderAttribute() { }
}
