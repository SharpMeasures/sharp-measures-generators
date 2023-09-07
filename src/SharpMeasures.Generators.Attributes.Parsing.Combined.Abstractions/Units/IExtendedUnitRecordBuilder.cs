namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="IExtendedUnitRecord"/>.</summary>
public interface IExtendedUnitRecordBuilder : IRecordBuilder<IExtendedUnitRecord>
{
    /// <summary>Specifies the original unit.</summary>
    /// <param name="original">The original unit, of which the unit is an extension.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOriginal(ITypeSymbol original, ExpressionSyntax syntax);
}
