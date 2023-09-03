namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface ISyntacticQuantityDifferenceRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the quantity that is the difference between two instances of the implementing quantity.</summary>
    public abstract ExpressionSyntax Difference { get; }
}
