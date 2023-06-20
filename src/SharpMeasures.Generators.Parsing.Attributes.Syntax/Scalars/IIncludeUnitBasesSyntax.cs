namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
public interface IIncludeUnitBasesSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the names of the included unit instances.</summary>
    public abstract Location UnitInstancesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the included unit instances.</summary>
    public abstract IReadOnlyList<Location> UnitInstancesElements { get; }
}
