namespace SharpMeasures.Generators.Attributes.Vectors;

using OneOf;
using OneOf.Types;

using System.Collections.Generic;

/// <summary>Represents a <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface ISemanticVectorComponentNamesRecord
{
    /// <summary>The names of the Cartesian components.</summary>
    public abstract OneOf<None, IReadOnlyList<string?>?> Names { get; }

    /// <summary>The expression used to derive the name of each Cartesian component.</summary>
    public abstract OneOf<None, string?> Expression { get; }
}
