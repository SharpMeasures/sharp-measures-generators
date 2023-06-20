namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, declaring that the quantity does not support addition of two instances.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DisableQuantitySumAttribute : Attribute { }
