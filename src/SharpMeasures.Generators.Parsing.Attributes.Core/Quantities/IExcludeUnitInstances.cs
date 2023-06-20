namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="ExcludeUnitInstancesAttribute"/>.</summary>
public interface IExcludeUnitInstances
{
    /// <summary>The names of the unit instances that are excluded.</summary>
    public abstract IReadOnlyList<string?>? UnitInstances { get; }
}
