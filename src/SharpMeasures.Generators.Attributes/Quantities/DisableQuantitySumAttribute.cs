namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, declaring that the marked quantity does not support addition of two instances.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DisableQuantitySumAttribute : Attribute
{
    /// <summary>Declares that the marked quantity does not support addition of two instances.</summary>
    public DisableQuantitySumAttribute() { }
}
