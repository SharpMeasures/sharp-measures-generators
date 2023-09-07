namespace SharpMeasures.Generators.Attributes.Vectors;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
public interface ISemanticNegativeMagnitudeBehaviourRecord
{
    /// <summary>The behaviour of the constructor of the quantity.</summary>
    public abstract OneOf<None, DisallowNegativeBehaviour> Behaviour { get; }
}
