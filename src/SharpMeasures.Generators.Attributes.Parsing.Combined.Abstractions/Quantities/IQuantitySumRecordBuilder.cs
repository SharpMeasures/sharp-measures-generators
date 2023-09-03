namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="IQuantitySumRecord"/>.</summary>
public interface IQuantitySumRecordBuilder : IRecordBuilder<IQuantitySumRecord>
{
    /// <summary>Specifies the quantity that represents the sum of two instances of the implementing quantity.</summary>
    /// <param name="sum">The quantity that represents the sum of two instances of the implementing quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithSum(ITypeSymbol sum, ExpressionSyntax syntax);
}
