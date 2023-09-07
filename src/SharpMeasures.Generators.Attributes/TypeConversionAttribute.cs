namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures types, declaring that the marked tyoe supports conversion to and/or from the listed types.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class TypeConversionAttribute : Attribute
{
    /// <summary>The set of types to and/or from which the marked type supports conversion.</summary>
    public Type[] Types { get; }

    /// <summary>Determines how the conversions from the marked type to the provided types are implemented.</summary>
    public ConversionImplementation ForwardsImplementation { get; init; }

    /// <summary>The behaviour of the operators converting from the marked type to the provided types, if implemented.</summary>
    public ConversionOperatorBehaviour ForwardsBehaviour { get; init; }

    /// <summary>The name of the property converting from the marked type to the provided types, if implemented.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each type.</remarks>
    public string? ForwardsPropertyName { get; init; }

    /// <summary>The name of the instance methods converting from the marked type to the provided types, if implemented.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each type.</remarks>
    public string? ForwardsMethodName { get; init; }

    /// <summary>The name of the static method converting from the marked type to the provided types, if implemented.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each type.</remarks>
    public string? ForwardsStaticMethodName { get; init; }

    /// <summary>Determines how the conversions from the provided types to the marked type are implemented.</summary>
    public ConversionImplementation BackwardsImplementation { get; init; }

    /// <summary>The behaviour of the operators converting from the provided types to the marked type, if implemented.</summary>
    public ConversionOperatorBehaviour BackwardsBehaviour { get; init; }

    /// <summary>The name of the static method converting from the provided types to the marked type, if implemented.</summary>
    /// <remarks>Occurences of "{T}" are replaced by the name of each type.</remarks>
    public string? BackwardsStaticMethodName { get; init; }

    /// <summary>Declares that the marked type support conversion to and/or from the listed types.</summary>
    /// <param name="types"><inheritdoc cref="Types" path="/summary"/></param>
    public TypeConversionAttribute(params Type[] types)
    {
        Types = types;
    }
}
