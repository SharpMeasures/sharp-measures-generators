namespace SharpMeasures.Generators.Attributes.Scalars;

using OneOf;
using OneOf.Types;

/// <summary>Represents the arguments of a <see cref="DisallowNegativeAttribute"/>.</summary>
public interface ISemanticDisallowNegativeRecord
{
    /// <summary>The behaviour of the constructor of the quantity.</summary>
    public abstract OneOf<None, DisallowNegativeBehaviour> Behaviour { get; }
}
