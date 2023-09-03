namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="IScalarQuantityRecord"/>.</summary>
public interface IScalarQuantityRecordBuilder : IRecordBuilder<IScalarQuantityRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithUnit(ITypeSymbol unit, ExpressionSyntax syntax);

    /// <summary>Specifies whether the quantity is biased.</summary>
    /// <param name="biased">Indicates whether the quantity is biased.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBiased(bool biased, ExpressionSyntax syntax);
}
