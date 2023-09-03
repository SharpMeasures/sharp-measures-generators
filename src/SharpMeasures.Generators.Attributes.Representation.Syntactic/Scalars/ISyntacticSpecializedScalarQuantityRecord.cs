namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
public interface ISyntacticSpecializedScalarQuantityRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the original scalar quantity, of which this quantity is a specialized form.</summary>
    public abstract ExpressionSyntax Original { get; }
}
