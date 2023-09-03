namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface ISyntacticSpecializedVectorQuantityRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the original vector quantity, of which this quantity is a specialized form.</summary>
    public abstract ExpressionSyntax Original { get; }
}
