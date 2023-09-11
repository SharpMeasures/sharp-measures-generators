namespace SharpMeasures;

using System;

/// <summary>Declares the marked property as a constant of the containing quantity.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class QuantityConstantAttribute : Attribute
{
    /// <summary>Declares the marked property as a constant of the containing quantity.</summary>
    public QuantityConstantAttribute() { }
}
