namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="VectorQuantityAttribute{TUnit}"/>, with syntactical information.</summary>
public interface ISyntacticVectorQuantity : IVectorQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    public abstract IVectorQuantitySyntax Syntax { get; }
}
