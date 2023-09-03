namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="IScalarAssociationRecord"/>.</summary>
public interface IScalarAssociationRecordBuilder : IRecordBuilder<IScalarAssociationRecord>
{
    /// <summary>Specifies the scalar quantity associated with the vector quantity.</summary>
    /// <param name="scalarQuantity">The scalar quantity associated with the vector quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithScalarQuantity(ITypeSymbol scalarQuantity, ExpressionSyntax syntax);

    /// <summary>Specifies whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    /// <param name="asComponents">Indicates whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithAsComponents(bool asComponents, ExpressionSyntax syntax);

    /// <summary>Specifies whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</summary>
    /// <param name="asMagnitude">Indicates whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithAsMagnitude(bool asMagnitude, ExpressionSyntax syntax);
}
