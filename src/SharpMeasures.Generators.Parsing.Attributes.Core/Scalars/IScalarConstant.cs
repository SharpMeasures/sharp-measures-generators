namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using OneOf;

/// <summary>Represents a parsed <see cref="ScalarConstantAttribute"/>.</summary>
public interface IScalarConstant
{
    /// <summary>The name of the constant.</summary>
    public abstract string? Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public abstract string? UnitInstanceName { get; }

    /// <summary>The value of the constant, when expressed in the provided unit instance.</summary>
    public abstract OneOf<double, string?> Value { get; }
}
