namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="DisallowNegativeAttribute"/>.</summary>
public interface ISyntacticDisallowNegativeRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the behaviour of the quantity constructor.</summary>
    public abstract OneOf<None, ExpressionSyntax> Behaviour { get; }
}
