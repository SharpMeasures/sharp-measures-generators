namespace SharpMeasures.Generators.Attributes.Quantities;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface ISemanticDefaultUnitInstanceRecord
{
    /// <summary>The name of the default unit instance.</summary>
    public abstract string? UnitInstance { get; }

    /// <summary>The symbol representing the default unit instance.</summary>
    public abstract OneOf<None, string?> Symbol { get; }
}
