namespace SharpMeasures.Generators.Attributes.Units;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="UnitInstanceAttribute"/>.</summary>
public interface ISemanticUnitInstanceRecord
{
    /// <summary>The name of the unit instance.</summary>
    public abstract OneOf<None, string?> Name { get; }

    /// <summary>The plural form of the name of the unit instance.</summary>
    public abstract OneOf<None, string?> PluralForm { get; }
}
