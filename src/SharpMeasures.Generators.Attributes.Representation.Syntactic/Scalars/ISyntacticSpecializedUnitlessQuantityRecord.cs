namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
public interface ISyntacticSpecializedUnitlessQuantityRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the original quantity, of which this quantity is a specialized form.</summary>
    public abstract ExpressionSyntax Original { get; }
}
