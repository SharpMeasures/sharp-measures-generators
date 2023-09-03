namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="IVectorGroupRecord"/>.</summary>
public interface IVectorGroupRecordBuilder : IRecordBuilder<IVectorGroupRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithUnit(ITypeSymbol unit, ExpressionSyntax syntax);
}
