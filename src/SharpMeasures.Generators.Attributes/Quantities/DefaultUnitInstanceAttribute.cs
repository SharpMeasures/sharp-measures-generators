namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, describing the default unit instance of the quantity - used when formatting the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DefaultUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the default unit instance.</summary>
    public string UnitInstance { get; }

    /// <summary>The symbol representing the default unit instance, or <see langword="null"/> to use the full name of the unit instance.</summary>
    public string? Symbol { get; }

    /// <inheritdoc cref="DefaultUnitInstanceAttribute"/>
    /// <param name="unitInstance"><inheritdoc cref="UnitInstance" path="/summary"/></param>
    /// <param name="symbol"><inheritdoc cref="Symbol" path="/summary"/></param>
    public DefaultUnitInstanceAttribute(string unitInstance, string symbol)
    {
        UnitInstance = unitInstance;
        Symbol = symbol;
    }

    /// <inheritdoc cref="DefaultUnitInstanceAttribute"/>
    /// <param name="unitInstance"><inheritdoc cref="UnitInstance" path="/summary"/></param>
    public DefaultUnitInstanceAttribute(string unitInstance)
    {
        UnitInstance = unitInstance;
    }
}
