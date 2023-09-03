namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using System.Collections.Generic;

/// <summary>Represents syntactic information about a <see cref="QuantityConversionAttribute"/>.</summary>
public interface ISyntacticQuantityConversionRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the target quantities, or of each element in the argument if expressed as a <see langword="params"/>-array.</summary>
    public abstract OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> Quantities { get; }

    /// <summary>The syntactic description of the argument for how the conversions from the implementing quantity to the provided quantities are implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsImplementation { get; }

    /// <summary>The syntactic description of the argument for the behaviour of the operators converting from the implementing quantity to the provided quantities.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsBehaviour { get; }

    /// <summary>The syntactic description of the argument for the name of the properties converting from the implementing quantity to the provided quantities.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsPropertyName { get; }

    /// <summary>The syntactic description of the argument for the name of the instance method converting from the implementing quantity to the provided quantities.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsMethodName { get; }

    /// <summary>The syntactic description of the argument for the name of the static method converting from the implementing quantity to the provided quantities.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsStaticMethodName { get; }

    /// <summary>The syntactic description of the argument for how the conversions from the provided quantities to the implementing quantity are implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsImplementation { get; }

    /// <summary>The syntactic description of the argument for the behaviour of the operators converting from provided quantities to the implementing quantity.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsBehaviour { get; }

    /// <summary>The syntactic description of the argument for the name of the static method converting from the provided quantities to the implementing quantity.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsStaticMethodName { get; }
}
