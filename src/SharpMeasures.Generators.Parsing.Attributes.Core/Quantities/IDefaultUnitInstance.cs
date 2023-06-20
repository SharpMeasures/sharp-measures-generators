namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface IDefaultUnitInstance
{
    /// <summary>The name of the default unit instance.</summary>
    public abstract string? UnitInstance { get; }

    /// <summary>The symbol representing the default unit instance.</summary>
    public abstract string? Symbol { get; }
}
