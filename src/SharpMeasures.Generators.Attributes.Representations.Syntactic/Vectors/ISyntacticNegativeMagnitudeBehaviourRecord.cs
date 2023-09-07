namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
public interface ISyntacticNegativeMagnitudeBehaviourRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the behaviour of the quantity constructor.</summary>
    public abstract OneOf<None, ExpressionSyntax> Behaviour { get; }
}
