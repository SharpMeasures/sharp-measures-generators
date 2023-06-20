namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="QuantityConversionAttribute"/>.</summary>
public interface IQuantityConversionSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the target quantities.</summary>
    public abstract Location QuantitiesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the target quantities.</summary>
    public abstract IReadOnlyList<Location> QuantitiesElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversions from the implementing quantity to the provided quantities are implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsImplementation { get; }

    /// <summary>The <see cref="Location"/> of the argument for the behaviour of the operators converting from the implementing quantity to the provided quantities. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsBehaviour { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the properties converting from the implementing quantity to the provided quantities. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsPropertyName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the instance method converting from the implementing quantity to the provided quantities. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsMethodName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the static method converting from the implementing quantity to the provided quantities. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsStaticMethodName { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversions from the provided quantities to the implementing quantity are implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BackwardsImplementation { get; }

    /// <summary>The <see cref="Location"/> of the argument for the behaviour of the operators converting from provided quantities to the implementing quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BackwardsBehaviour { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the static method converting from the provided quantities to the implementing quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BackwardsStaticMethodName { get; }
}
