namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISpecializedUnitlessQuantityRecord"/>.</summary>
public interface ISpecializedUnitlessQuantityRecordBuilder : IRecordBuilder<ISpecializedUnitlessQuantityRecord>
{
    /// <summary>Specifies the original quantity.</summary>
    /// <param name="original">The original quantity, of which the quantity is a specialized form.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOriginal(ITypeSymbol original, ExpressionSyntax syntax);
}
