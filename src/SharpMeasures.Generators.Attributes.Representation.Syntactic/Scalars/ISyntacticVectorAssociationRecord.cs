namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface ISyntacticVectorAssociationRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the vector quantity associated with the scalar quantity.</summary>
    public abstract ExpressionSyntax VectorQuantity { get; }
}
