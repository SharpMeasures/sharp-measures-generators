namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="DisallowNegativeAttribute"/>.</summary>
public interface IDisallowNegative
{
    /// <summary>The <see cref="DisallowNegativeBehaviour"/> of the quantity constructor.</summary>
    public abstract DisallowNegativeBehaviour? Behaviour { get; }
}
