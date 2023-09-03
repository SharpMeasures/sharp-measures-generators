namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorQuantityRecord : ISemanticSpecializedVectorQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISyntacticSpecializedVectorQuantityRecord Syntactic { get; }
}
