namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
public interface INegativeMagnitudeBehaviourRecord : ISemanticNegativeMagnitudeBehaviourRecord
{
    /// <summary>Represents syntactic information about the <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
    public abstract ISyntacticNegativeMagnitudeBehaviourRecord Syntactic { get; }
}
