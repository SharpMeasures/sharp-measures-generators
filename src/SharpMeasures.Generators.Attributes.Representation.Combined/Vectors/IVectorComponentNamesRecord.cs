namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface IVectorComponentNamesRecord : ISemanticVectorComponentNamesRecord
{
    /// <summary>Represents syntactic information about the <see cref="VectorComponentNamesAttribute"/>.</summary>
    public abstract ISyntacticVectorComponentNamesRecord Syntactic { get; }
}
