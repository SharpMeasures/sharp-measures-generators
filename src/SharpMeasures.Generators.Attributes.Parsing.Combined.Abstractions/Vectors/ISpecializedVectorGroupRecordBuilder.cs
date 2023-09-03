namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISpecializedVectorGroupRecord"/>.</summary>
public interface ISpecializedVectorGroupRecordBuilder : IRecordBuilder<ISpecializedVectorGroupRecord>
{
    /// <summary>Specifies the original group.</summary>
    /// <param name="original">The original group, of which the group is a specialized form.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOriginal(ITypeSymbol original, ExpressionSyntax syntax);
}
