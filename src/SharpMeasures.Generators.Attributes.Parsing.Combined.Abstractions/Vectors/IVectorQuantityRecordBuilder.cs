namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="IVectorQuantityRecord"/>.</summary>
public interface IVectorQuantityRecordBuilder : IRecordBuilder<IVectorQuantityRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithUnit(ITypeSymbol unit, ExpressionSyntax syntax);

    /// <summary>Specifies the dimension of the vector space.</summary>
    /// <param name="dimension">The dimension of the vector space.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithDimension(int dimension, ExpressionSyntax syntax);
}
