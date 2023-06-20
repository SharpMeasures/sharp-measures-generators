namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface IVectorComponentNames
{
    /// <summary>The names of the Cartesian components.</summary>
    public abstract IReadOnlyList<string?>? Names { get; }

    /// <summary>The expression used to derive the name of each Cartesian component.</summary>
    public abstract string? Expression { get; }
}
