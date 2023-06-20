namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
public interface IBiasedUnitInstance : IModifiedUnitInstance
{
    /// <summary>The bias relative to the original unit instance.</summary>
    public abstract OneOf<double, string?> Bias { get; }
}
