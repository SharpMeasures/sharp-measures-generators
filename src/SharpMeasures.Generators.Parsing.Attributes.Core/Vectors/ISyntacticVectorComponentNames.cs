namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="VectorComponentNamesAttribute"/>, with syntactical information.</summary>
public interface ISyntacticVectorComponentNames : IVectorComponentNames
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
    public abstract IVectorComponentNamesSyntax Syntax { get; }
}
