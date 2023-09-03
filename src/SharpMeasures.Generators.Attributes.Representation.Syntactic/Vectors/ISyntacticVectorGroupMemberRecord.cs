namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface ISyntacticVectorGroupMemberRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the group that the member belongs to.</summary>
    public abstract ExpressionSyntax Group { get; }

    /// <summary>The syntactic description of the argument for the dimension of the vector space.</summary>
    public abstract OneOf<None, ExpressionSyntax> Dimension { get; }
}
