namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface ISyntacticVectorComponentNamesRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the names of the Cartesian components.</summary>
    public abstract OneOf<None, ExpressionSyntax> Names { get; }

    /// <summary>The syntactic description of the argument for expression used to derive the name of each Cartesian component.</summary>
    public abstract OneOf<None, ExpressionSyntax> Expression { get; }
}
