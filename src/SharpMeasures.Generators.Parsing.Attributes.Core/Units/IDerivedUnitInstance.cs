namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
public interface IDerivedUnitInstance : IUnitInstance
{
    /// <summary>The ID of the intended derivation signature, if provided.</summary>
    public abstract string? DerivationID { get; }

    /// <summary>The names of the unit instances of other units from which the unit instance is derived.</summary>
    public abstract IReadOnlyList<string?>? UnitInstances { get; }
}
