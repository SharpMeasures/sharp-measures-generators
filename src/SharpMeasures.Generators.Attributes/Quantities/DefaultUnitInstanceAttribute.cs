namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, declaring the default unit instance of the marked quantity - used when formatting the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DefaultUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the default unit instance.</summary>
    public string UnitInstance { get; }

    /// <summary>The symbol representing the default unit instance. By default, the full name of the unit instance will be used.</summary>
    public string? Symbol { get; init; }

    /// <summary>Declares the default unit instance of the marked quantity.</summary>
    /// <param name="unitInstance"><inheritdoc cref="UnitInstance" path="/summary"/></param>
    public DefaultUnitInstanceAttribute(string unitInstance)
    {
        UnitInstance = unitInstance;
    }
}
