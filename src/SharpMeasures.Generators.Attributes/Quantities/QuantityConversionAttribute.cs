namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, declaring that the marked quantity supports conversion to and/or from the listed quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class QuantityConversionAttribute : Attribute
{
    /// <summary>The set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    public Type[] Quantities { get; }

    /// <summary>Determines how the conversions from the implementing quantity to the provided quantities are implemented. The default behaviour is { <see cref="ConversionImplementation.Property"/> | <see cref="ConversionImplementation.Operator"/> }.</summary>
    public ConversionImplementation ForwardsImplementation { get; init; }

    /// <summary>The behaviour of the operators converting from the implementing quantity to the provided quantities, if implemented. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsBehaviour { get; init; }

    /// <summary>The name of the property converting from the implementing quantity to the provided quantities, if implemented. By default, the name will be <i>AsT</i>, with <i>T</i> being the name of the target quantity.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each quantity.</remarks>
    public string? ForwardsPropertyName { get; init; }

    /// <summary>The name of the instance methods converting from the implementing quantity to the provided quantities, if implemented. By default, the name will be <i>ToT</i>, with <i>T</i> being the name of the target quantity.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each quantity.</remarks>
    public string? ForwardsMethodName { get; init; }

    /// <summary>The name of the static method converting from the implementing quantity to the provided quantities, if implemented. By default, the name will be <i>AsT</i>, with <i>T</i> being the name of the target quantity.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each quantity.</remarks>
    public string? ForwardsStaticMethodName { get; init; }

    /// <summary>Determines how the conversions from the provided quantities to the implementing quantity are implemented. The default behaviour is { <see cref="ConversionImplementation.StaticMethod"/> | <see cref="ConversionImplementation.Operator"/> }.</summary>
    public ConversionImplementation BackwardsImplementation { get; init; }

    /// <summary>The behaviour of the operators converting from the provided quantities to the implementing quantity, if implemented. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsBehaviour { get; init; }

    /// <summary>The name of the static method converting from the provided quantities to the implementing quantity, if implemented. By default, the name will be <i>From</i>.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each quantity.</remarks>
    public string? BackwardsStaticMethodName { get; init; }

    /// <summary>Declares that the marked quantity support conversion to and/or from the listed quantities.</summary>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/></param>
    public QuantityConversionAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
