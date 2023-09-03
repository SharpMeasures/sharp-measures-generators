namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Declares the marked type as an auto-generated SharpMeasures unit.</summary>
/// <typeparam name="TScalar">The scalar quantity that the unit describes.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class UnitAttribute<TScalar> : Attribute
{
    /// <summary>Determines whether the unit should include a bias term. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>A bias term allows units to use different definitions of zero. For example; <i>UnitOfTemperature</i> would require a bias term to express <i>Celsius</i> and <i>Fahrenheit</i>.</remarks>
    public bool BiasTerm { get; init; }

    /// <summary>Declares the marked type as an auto-generated SharpMeasures unit.</summary>
    public UnitAttribute() { }
}
