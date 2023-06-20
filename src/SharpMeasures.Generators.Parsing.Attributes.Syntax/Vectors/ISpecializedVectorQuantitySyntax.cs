namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorQuantitySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the original vector quantity, of which this quantity is a specialized form.</summary>
    public abstract Location Original { get; }
}
