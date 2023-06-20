namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>, with syntactical information.</summary>
public interface ISyntacticSpecializedVectorGroup : ISpecializedVectorGroup
{
    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedVectorGroupSyntax Syntax { get; }
}
