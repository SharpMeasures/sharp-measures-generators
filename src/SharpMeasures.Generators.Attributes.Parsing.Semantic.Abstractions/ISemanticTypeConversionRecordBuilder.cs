namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes;

using System.Collections.Generic;

/// <summary>Handles incremental construction of <see cref="ISemanticTypeConversionRecord"/>.</summary>
public interface ISemanticTypeConversionRecordBuilder : IRecordBuilder<ISemanticTypeConversionRecord>
{
    /// <summary>Specifies the set of types to and/or from which the marked type supports conversion.</summary>
    /// <param name="types">The set of types to and/or from which the marked type supports conversion.</param>
    public abstract void WithTypes(IReadOnlyList<ITypeSymbol?>? types);

    /// <summary>Specifies how the conversions from the marked type to the provided types are implemented.</summary>
    /// <param name="forwardsImplementation">Determines how the conversions from the marked type to the provided types are implemented.</param>
    public abstract void WithForwardsImplementation(ConversionImplementation forwardsImplementation);

    /// <summary>Specifies the behaviour of the operators converting from the marked type to the provided types.</summary>
    /// <param name="forwardsBehaviour">The behaviour of the operators converting from the marked type to the provided types.</param>
    public abstract void WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour);

    /// <summary>Specifies the name of the property converting from the marked type to the provided types.</summary>
    /// <param name="forwardsPropertyName">The name of the property converting from the marked type to the provided types.</param>
    public abstract void WithForwardsPropertyName(string? forwardsPropertyName);

    /// <summary>Specifies the name of the instance methods converting from the marked type to the provided types.</summary>
    /// <param name="forwardsMethodName">The name of the instance methods converting from the marked type to the provided types.</param>
    public abstract void WithForwardsMethodName(string? forwardsMethodName);

    /// <summary>Specifies the name of the static method converting from the marked type to the provided types.</summary>
    /// <param name="forwardsStaticMethodName">The name of the static method converting from the marked type to the provided types.</param>
    public abstract void WithForwardsStaticMethodName(string? forwardsStaticMethodName);

    /// <summary>Specifies how the conversions from the provided types to the marked type are implemented.</summary>
    /// <param name="backwardsImplementation">Determines how the conversions from the provided types to the marked type are implemented.</param>
    public abstract void WithBackwardsImplementation(ConversionImplementation backwardsImplementation);

    /// <summary>Specifies the behaviour of the operators converting from the provided types to the marked type.</summary>
    /// <param name="backwardsBehaviour">The behaviour of the operators converting from the provided types to the marked type.</param>
    public abstract void WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour);

    /// <summary>Specifies the name of the static method converting from the provided types to the marked type.</summary>
    /// <param name="backwardsStaticMethodName">The name of the static method converting from the provided types to the marked type.</param>
    public abstract void WithBackwardsStaticMethodName(string? backwardsStaticMethodName);
}
