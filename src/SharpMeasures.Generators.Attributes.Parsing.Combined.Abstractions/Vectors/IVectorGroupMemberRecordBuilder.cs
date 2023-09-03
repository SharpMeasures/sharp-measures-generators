namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="IVectorGroupMemberRecord"/>.</summary>
public interface IVectorGroupMemberRecordBuilder : IRecordBuilder<IVectorGroupMemberRecord>
{
    /// <summary>Specifies the group that the member belongs to.</summary>
    /// <param name="group">The group that the member belongs to.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithGroup(ITypeSymbol group, ExpressionSyntax syntax);

    /// <summary>Specifies the dimension of the vector space.</summary>
    /// <param name="dimension">The dimension of the vector space.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithDimension(int dimension, ExpressionSyntax syntax);
}
