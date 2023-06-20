namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
public interface IScaledUnitInstance : IModifiedUnitInstance
{
    /// <summary>The value by which the original unit instance is scaled.</summary>
    public abstract OneOf<double, string?> Scale { get; }
}
