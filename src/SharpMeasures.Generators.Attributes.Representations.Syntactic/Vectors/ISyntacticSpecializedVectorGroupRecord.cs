namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISyntacticSpecializedVectorGroupRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the original group, of which this group is a specialized form.</summary>
    public abstract ExpressionSyntax Original { get; }
}
