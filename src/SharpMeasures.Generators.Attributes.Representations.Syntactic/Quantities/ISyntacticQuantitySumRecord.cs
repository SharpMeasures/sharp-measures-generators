namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface ISyntacticQuantitySumRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the quantity that is the sum of two instances of the implementing quantity.</summary>
    public abstract ExpressionSyntax Sum { get; }
}
