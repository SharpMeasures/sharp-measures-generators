namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorGroupRecord : ISemanticSpecializedVectorGroupRecord
{
    /// <summary>Represents syntactic information about the <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    public abstract ISyntacticSpecializedVectorGroupRecord Syntactic { get; }
}
