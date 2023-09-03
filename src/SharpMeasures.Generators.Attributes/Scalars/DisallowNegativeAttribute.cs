namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, declaring that the marked quantity may not be negative.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class DisallowNegativeAttribute : Attribute
{
    /// <summary>Determines the behaviour of the constructor of the quantity when provided a negative value.</summary>
    public DisallowNegativeBehaviour Behaviour { get; }

    /// <summary>Declares that the marked quantity may not be negative.</summary>
    /// <param name="behaviour"><inheritdoc cref="Behaviour" path="/summary"/></param>
    public DisallowNegativeAttribute(DisallowNegativeBehaviour behaviour)
    {
        Behaviour = behaviour;
    }

    /// <summary>Declares that the marked quantity may not be negative.</summary>
    public DisallowNegativeAttribute() { }
}
