namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISpecializedVectorQuantityRecord"/>.</summary>
public interface ISpecializedVectorQuantityRecordBuilder : IRecordBuilder<ISpecializedVectorQuantityRecord>
{
    /// <summary>Specifies the original vector quantity.</summary>
    /// <param name="original">The original vector quantity, of which the quantity is a specialized form.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOriginal(ITypeSymbol original, ExpressionSyntax syntax);
}
