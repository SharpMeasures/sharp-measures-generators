namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>, with syntactical information.</summary>
public interface ISyntacticSpecializedVectorQuantity : ISpecializedVectorQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedVectorQuantitySyntax Syntax { get; }
}
