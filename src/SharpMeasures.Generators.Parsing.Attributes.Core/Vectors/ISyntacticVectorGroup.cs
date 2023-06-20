namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="VectorGroupAttribute{TUnit}"/>, with syntactical information.</summary>
public interface ISyntacticVectorGroup : IVectorGroup
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    public abstract IVectorGroupSyntax Syntax { get; }
}
