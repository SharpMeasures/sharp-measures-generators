namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="ScalarConstantAttribute"/>.</summary>
public interface IScalarConstantSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the constant.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the unit instance in which the provided value is expressed.</summary>
    public abstract Location UnitInstance { get; }

    /// <summary>The <see cref="Location"/> of the argument for the value of the constant.</summary>
    public abstract Location Value { get; }
}
