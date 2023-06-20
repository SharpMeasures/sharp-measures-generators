namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="QuantityConversionAttribute"/>.</summary>
public interface IQuantityConversion
{
    /// <summary>The set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    public abstract IReadOnlyList<ITypeSymbol?>? Quantities { get; }

    /// <summary>Determines how the conversions from the implementing quantity to the provided quantities are implemented.</summary>
    public abstract ConversionImplementation? ForwardsImplementation { get; }

    /// <summary>The behaviour of the operators converting from the implementing quantity to the provided quantities.</summary>
    public abstract ConversionOperatorBehaviour? ForwardsBehaviour { get; }

    /// <summary>The name of the property converting from the implementing quantity to the provided quantities.</summary>
    public abstract string? ForwardsPropertyName { get; }

    /// <summary>The name of the instance methods converting from the implementing quantity to the provided quantities.</summary>
    public abstract string? ForwardsMethodName { get; }

    /// <summary>The name of the static method converting from the implementing quantity to the provided quantities.</summary>
    public abstract string? ForwardsStaticMethodName { get; }

    /// <summary>Determines how the conversions from the provided quantities to the implementing quantity are implemented.</summary>
    public abstract ConversionImplementation? BackwardsImplementation { get; }

    /// <summary>The behaviour of the operators converting from the provided quantities to the implementing quantity.</summary>
    public abstract ConversionOperatorBehaviour? BackwardsBehaviour { get; }

    /// <summary>The name of the static method converting from the provided quantities to the implementing quantity.</summary>
    public abstract string? BackwardsStaticMethodName { get; }
}
