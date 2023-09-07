namespace SharpMeasures.Generators.Attributes;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using System.Collections.Generic;

/// <summary>Represents a <see cref="TypeConversionAttribute"/>.</summary>
public interface ISemanticTypeConversionRecord
{
    /// <summary>The set of types to and/or from which the marked type supports conversion.</summary>
    public abstract IReadOnlyList<ITypeSymbol?>? Types { get; }

    /// <summary>Determines how the conversions from the marked type to the provided types are implemented.</summary>
    public abstract OneOf<None, ConversionImplementation> ForwardsImplementation { get; }

    /// <summary>The behaviour of the operators converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, ConversionOperatorBehaviour> ForwardsBehaviour { get; }

    /// <summary>The name of the property converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, string?> ForwardsPropertyName { get; }

    /// <summary>The name of the instance methods converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, string?> ForwardsMethodName { get; }

    /// <summary>The name of the static method converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, string?> ForwardsStaticMethodName { get; }

    /// <summary>Determines how the conversions from the provided types to the marked type are implemented.</summary>
    public abstract OneOf<None, ConversionImplementation> BackwardsImplementation { get; }

    /// <summary>The behaviour of the operators converting from the provided types to the marked type.</summary>
    public abstract OneOf<None, ConversionOperatorBehaviour> BackwardsBehaviour { get; }

    /// <summary>The name of the static method converting from the provided types to the marked type.</summary>
    public abstract OneOf<None, string?> BackwardsStaticMethodName { get; }
}
