namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, dictating the set of unit instances for which a static property representing the magnitude { 1 } is implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="ExcludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitBasesAttribute : Attribute
{
    /// <summary>The names of the unit instances for which a static property representing the magnitude { 1 } is implemented.</summary>
    public string[] UnitInstances { get; }

    /// <inheritdoc cref="IncludeUnitBasesAttribute"/>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public IncludeUnitBasesAttribute(params string[] unitInstances)
    {
        UnitInstances = unitInstances;
    }
}
