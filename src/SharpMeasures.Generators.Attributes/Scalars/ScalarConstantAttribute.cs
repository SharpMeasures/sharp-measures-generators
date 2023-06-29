namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, defining a constant value of the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScalarConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public string UnitInstance { get; }

    /// <summary>The value of the constant, when expressed in the provided unit.</summary>
    public double Value { get; }

    /// <summary>The expression used to compute the value of the constant, when expressed in the provided unit.</summary>
    public string? Expression { get; }

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstance"><inheritdoc cref="UnitInstance" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
    public ScalarConstantAttribute(string name, string unitInstance, double value)
    {
        Name = name;
        UnitInstance = unitInstance;
        Value = value;
    }

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstance"><inheritdoc cref="UnitInstance" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <remarks>When applicable, prefer <see cref="ScalarConstantAttribute(string, string, double)"/>.</remarks>
    public ScalarConstantAttribute(string name, string unitInstance, string expression)
    {
        Name = name;
        UnitInstance = unitInstance;
        Expression = expression;
    }
}
