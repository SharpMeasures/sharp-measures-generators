namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface ISyntacticScalarAssociationRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the scalar quantity associated with the vector quantity.</summary>
    public abstract ExpressionSyntax ScalarQuantity { get; }

    /// <summary>The syntactic description of the argument for whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    public abstract OneOf<None, ExpressionSyntax> AsComponents { get; }

    /// <summary>The syntactic description of the argument for whether the scalar quantity should be used to describe the magnitude of the vector quantity.</summary>
    public abstract OneOf<None, ExpressionSyntax> AsMagnitude { get; }
}
