namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="IQuantityDifferenceRecord"/>.</summary>
public interface IQuantityDifferenceRecordBuilder : IRecordBuilder<IQuantityDifferenceRecord>
{
    /// <summary>Specifies the quantity that represents the difference between two instances of the implementing quantity.</summary>
    /// <param name="difference">The quantity that represents the difference between two instances of the implementing quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithDifference(ITypeSymbol difference, ExpressionSyntax syntax);
}
