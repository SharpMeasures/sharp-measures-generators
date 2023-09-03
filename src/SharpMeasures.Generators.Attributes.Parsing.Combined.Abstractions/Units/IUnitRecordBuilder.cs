namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="IUnitRecord"/>.</summary>
public interface IUnitRecordBuilder : IRecordBuilder<IUnitRecord>
{
    /// <summary>Specifies the scalar quantity primarily described by the unit.</summary>
    /// <param name="scalarQuantity">The scalar quantity primarily described by the unit.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithScalarQuantity(ITypeSymbol scalarQuantity, ExpressionSyntax syntax);

    /// <summary>Specifies whether the unit includes a bias term.</summary>
    /// <param name="biasTerm">Indicates whether the unit includes a bias term.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBiasTerm(bool biasTerm, ExpressionSyntax syntax);
}
