namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface ISemanticSpecializedVectorQuantityRecord
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original vector quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
