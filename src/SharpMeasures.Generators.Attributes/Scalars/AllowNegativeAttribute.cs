namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, declaring that the marked quantity may be negative.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class AllowNegativeAttribute : Attribute
{
    /// <summary>Declares that the marked quantity may be negative.</summary>
    public AllowNegativeAttribute() { }
}
