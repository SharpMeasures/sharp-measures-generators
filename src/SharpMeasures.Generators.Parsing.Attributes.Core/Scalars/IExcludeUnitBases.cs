namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
public interface IExcludeUnitBases
{
    /// <summary>The names of the unit instances that are excluded as bases.</summary>
    public abstract IReadOnlyList<string?>? UnitInstances { get; }
}
