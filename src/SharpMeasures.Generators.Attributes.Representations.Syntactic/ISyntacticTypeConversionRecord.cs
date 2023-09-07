namespace SharpMeasures.Generators.Attributes;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using System.Collections.Generic;

/// <summary>Represents syntactic information about a <see cref="TypeConversionAttribute"/>.</summary>
public interface ISyntacticTypeConversionRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the target types, or of each element in the argument if expressed as a <see langword="params"/>-array.</summary>
    public abstract OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> Types { get; }

    /// <summary>The syntactic description of the argument for how the conversions from the marked type to the provided types are implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsImplementation { get; }

    /// <summary>The syntactic description of the argument for the behaviour of the operators converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsBehaviour { get; }

    /// <summary>The syntactic description of the argument for the name of the properties converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsPropertyName { get; }

    /// <summary>The syntactic description of the argument for the name of the instance method converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsMethodName { get; }

    /// <summary>The syntactic description of the argument for the name of the static method converting from the marked type to the provided types.</summary>
    public abstract OneOf<None, ExpressionSyntax> ForwardsStaticMethodName { get; }

    /// <summary>The syntactic description of the argument for how the conversions from the provided types to the marked type are implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsImplementation { get; }

    /// <summary>The syntactic description of the argument for the behaviour of the operators converting from provided types to the marked type.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsBehaviour { get; }

    /// <summary>The syntactic description of the argument for the name of the static method converting from the provided types to the marked type.</summary>
    public abstract OneOf<None, ExpressionSyntax> BackwardsStaticMethodName { get; }
}
