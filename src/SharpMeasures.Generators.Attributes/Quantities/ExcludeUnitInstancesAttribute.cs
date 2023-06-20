namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, dictating the set of unit instances for which a property representing the magnitude is not implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="IncludeUnitInstancesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitInstancesAttribute : Attribute
{
    /// <summary>The names of the unit instances for which a property representing the magnitude is not implemented.</summary>
    public string[] UnitInstances { get; }

    /// <inheritdoc cref="ExcludeUnitInstancesAttribute"/>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public ExcludeUnitInstancesAttribute(params string[] unitInstances)
    {
        UnitInstances = unitInstances;
    }
}
