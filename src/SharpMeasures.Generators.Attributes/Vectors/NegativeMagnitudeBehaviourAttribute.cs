namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures vector quantities, specifying the behaviour of the marked quantity when constructed with a negative magnitude.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class NegativeMagnitudeBehaviourAttribute : Attribute
{
    /// <summary>Determines the behaviour of the quantity when constructed with a negative magnitude.</summary>
    public DisallowNegativeBehaviour Behaviour { get; }

    /// <summary>Specifies the behaviour of the marked quantity when constructed with a negative magnitude.</summary>
    /// <param name="behaviour"><inheritdoc cref="Behaviour" path="/summary"/></param>
    public NegativeMagnitudeBehaviourAttribute(DisallowNegativeBehaviour behaviour)
    {
        Behaviour = behaviour;
    }
}
