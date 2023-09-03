namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="IVectorAssociationRecord"/>.</summary>
public interface IVectorAssociationRecordBuilder : IRecordBuilder<IVectorAssociationRecord>
{
    /// <summary>Specifies the vector quantity associated with the scalar quantity.</summary>
    /// <param name="vectorQuantity">The vector quantity associated with the scalar quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithVectorQuantity(ITypeSymbol vectorQuantity, ExpressionSyntax syntax);
}
