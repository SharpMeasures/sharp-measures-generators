namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, dictating the set of unit instances for which a property representing the magnitude is implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="ExcludeUnitInstancesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitInstancesAttribute : Attribute
{
    /// <summary>The names of the unit instances for which a property representing the magnitude is implemented.</summary>
    public string[] UnitInstances { get; }

    /// <inheritdoc cref="IncludeUnitInstancesAttribute"/>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public IncludeUnitInstancesAttribute(params string[] unitInstances)
    {
        UnitInstances = unitInstances;
    }
}
