namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISemanticSpecializedVectorGroupRecord
{
    /// <summary>The original group, of which this group is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
