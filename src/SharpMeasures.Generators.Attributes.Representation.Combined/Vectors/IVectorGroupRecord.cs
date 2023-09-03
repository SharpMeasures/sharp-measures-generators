namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
public interface IVectorGroupRecord : ISemanticVectorGroupRecord
{
    /// <summary>Represents syntactic information about the <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    public abstract ISyntacticVectorGroupRecord Syntactic { get; }
}
