namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorGroupSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the original group, of which this group is a specialized form.</summary>
    public abstract Location Original { get; }
}
