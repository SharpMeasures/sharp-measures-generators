namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface ISyntacticScalarQuantityRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the unit that describes the quantity.</summary>
    public abstract ExpressionSyntax Unit { get; }

    /// <summary>The syntactic description of the argument for whether the quantity is biased.</summary>
    public abstract OneOf<None, ExpressionSyntax> Biased { get; }
}
