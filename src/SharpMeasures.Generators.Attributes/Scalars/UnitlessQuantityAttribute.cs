namespace SharpMeasures;

using System;

/// <summary>Declares the marked type as an auto-generated SharpMeasures unit-less quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnitlessQuantityAttribute : Attribute
{
    /// <summary>Declares the marked type as an auto-generated SharpMeasures unit-less quantity.</summary>
    public UnitlessQuantityAttribute() { }
}
