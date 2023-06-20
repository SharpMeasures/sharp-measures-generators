namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
public interface IIncludeUnitBases
{
    /// <summary>The names of the unit instances that are included as bases.</summary>
    public abstract IReadOnlyList<string?>? UnitInstances { get; }
}
