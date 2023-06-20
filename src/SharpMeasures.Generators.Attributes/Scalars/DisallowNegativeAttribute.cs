namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, declaring that the quantity may not be negative.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class DisallowNegativeAttribute : Attribute
{
    /// <summary>Determines the behaviour of the constructor of the quantity when provided a negative value. The default is <see cref="DisallowNegativeBehaviour.Absolute"/>.</summary>
    public DisallowNegativeBehaviour Behaviour { get; }

    /// <inheritdoc cref="DisallowNegativeAttribute"/>
    /// <param name="behaviour"><inheritdoc cref="Behaviour" path="/summary"/></param>
    public DisallowNegativeAttribute(DisallowNegativeBehaviour behaviour)
    {
        Behaviour = behaviour;
    }

    /// <inheritdoc cref="DisallowNegativeAttribute"/>
    public DisallowNegativeAttribute() { }
}
