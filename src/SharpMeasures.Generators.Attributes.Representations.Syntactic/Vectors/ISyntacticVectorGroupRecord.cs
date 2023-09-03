namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
public interface ISyntacticVectorGroupRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the unit that describes the quantity.</summary>
    public abstract ExpressionSyntax Unit { get; }
}
