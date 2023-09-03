namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface ISyntacticVectorQuantityRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the unit that describes the quantity.</summary>
    public abstract ExpressionSyntax Unit { get; }

    /// <summary>The syntactic description of the argument for the dimension of the vector space.</summary>
    public abstract OneOf<None, ExpressionSyntax> Dimension { get; }
}
