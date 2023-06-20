namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorGroup
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original group, of which this group is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
