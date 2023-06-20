namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
public interface IPrefixedUnitInstance : IModifiedUnitInstance
{
    /// <summary>The prefix that is applied to the original unit instance.</summary>
    public abstract OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }
}
