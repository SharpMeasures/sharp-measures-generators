namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="IncludeUnitInstancesAttribute"/>.</summary>
public interface IIncludeUnitInstances
{
    /// <summary>The names of the unit instances that are included.</summary>
    public abstract IReadOnlyList<string?>? UnitInstances { get; }
}
