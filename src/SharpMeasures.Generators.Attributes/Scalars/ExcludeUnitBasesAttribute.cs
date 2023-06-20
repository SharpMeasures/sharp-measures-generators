namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, dictating the set of unit instances for which a static property representing the magnitude { 1 } is not implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="IncludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitBasesAttribute : Attribute
{
    /// <summary>The names of the unit instances for which a static property representing the magnitude { 1 } is not implemented.</summary>
    public string[] UnitInstances { get; }

    /// <inheritdoc cref="ExcludeUnitBasesAttribute"/>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public ExcludeUnitBasesAttribute(params string[] unitInstances)
    {
        UnitInstances = unitInstances;
    }
}
