namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="VectorConstantAttribute"/>, with syntactical information.</summary>
public interface ISyntacticVectorConstant : IVectorConstant
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorConstantAttribute"/>.</summary>
    public abstract IVectorConstantSyntax Syntax { get; }
}
