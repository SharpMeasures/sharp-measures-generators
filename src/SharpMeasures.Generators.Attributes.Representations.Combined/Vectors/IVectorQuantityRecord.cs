namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface IVectorQuantityRecord : ISemanticVectorQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    public abstract ISyntacticVectorQuantityRecord Syntactic { get; }
}
