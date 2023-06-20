namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures scalar quantity.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class ScalarQuantityAttribute<TUnit> : Attribute
{
    /// <summary>Dictates whether quantity should consider the bias term of the unit. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>For example; <i>Temperature</i> would need to be biased to express both <i>Celsius</i> and <i>Fahrenheit</i>, while <i>TemperatureDifference</i> would not.</remarks>
    public bool Biased { get; init; }

    /// <inheritdoc cref="ScalarQuantityAttribute{TUnit}"/>
    public ScalarQuantityAttribute() { }
}
