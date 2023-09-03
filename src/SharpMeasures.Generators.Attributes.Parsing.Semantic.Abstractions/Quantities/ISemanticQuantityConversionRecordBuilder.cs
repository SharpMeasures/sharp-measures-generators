namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

using System.Collections.Generic;

/// <summary>Handles incremental construction of <see cref="ISemanticQuantityConversionRecord"/>.</summary>
public interface ISemanticQuantityConversionRecordBuilder : IRecordBuilder<ISemanticQuantityConversionRecord>
{
    /// <summary>Specifies the set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    /// <param name="quantities">The set of quantities to and/or from which the implementing quantity supports conversion.</param>
    public abstract void WithQuantities(IReadOnlyList<ITypeSymbol?>? quantities);

    /// <summary>Specifies how the conversions from the implementing quantity to the provided quantities are implemented.</summary>
    /// <param name="forwardsImplementation">Determines how the conversions from the implementing quantity to the provided quantities are implemented.</param>
    public abstract void WithForwardsImplementation(ConversionImplementation forwardsImplementation);

    /// <summary>Specifies the behaviour of the operators converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsBehaviour">The behaviour of the operators converting from the implementing quantity to the provided quantities.</param>
    public abstract void WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour);

    /// <summary>Specifies the name of the property converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsPropertyName">The name of the property converting from the implementing quantity to the provided quantities.</param>
    public abstract void WithForwardsPropertyName(string? forwardsPropertyName);

    /// <summary>Specifies the name of the instance methods converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsMethodName">The name of the instance methods converting from the implementing quantity to the provided quantities.</param>
    public abstract void WithForwardsMethodName(string? forwardsMethodName);

    /// <summary>Specifies the name of the static method converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsStaticMethodName">The name of the static method converting from the implementing quantity to the provided quantities.</param>
    public abstract void WithForwardsStaticMethodName(string? forwardsStaticMethodName);

    /// <summary>Specifies how the conversions from the provided quantities to the implementing quantity are implemented.</summary>
    /// <param name="backwardsImplementation">Determines how the conversions from the provided quantities to the implementing quantity are implemented.</param>
    public abstract void WithBackwardsImplementation(ConversionImplementation backwardsImplementation);

    /// <summary>Specifies the behaviour of the operators converting from the provided quantities to the implementing quantity.</summary>
    /// <param name="backwardsBehaviour">The behaviour of the operators converting from the provided quantities to the implementing quantity.</param>
    public abstract void WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour);

    /// <summary>Specifies the name of the static method converting from the provided quantities to the implementing quantity.</summary>
    /// <param name="backwardsStaticMethodName">The name of the static method converting from the provided quantities to the implementing quantity.</param>
    public abstract void WithBackwardsStaticMethodName(string? backwardsStaticMethodName);
}
