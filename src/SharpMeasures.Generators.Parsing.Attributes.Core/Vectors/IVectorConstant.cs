namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using OneOf;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="VectorConstantAttribute"/>.</summary>
public interface IVectorConstant
{
    /// <summary>The name of the constant.</summary>
    public abstract string? Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public abstract string? UnitInstance { get; }

    /// <summary>The value of the constant, when expressed in the provided unit instance.</summary>
    public abstract OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }
}
