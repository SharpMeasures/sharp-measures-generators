namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IUnitSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the scalar quantity primarily described by the unit.</summary>
    public abstract Location ScalarQuantity { get; }

    /// <summary>The <see cref="Location"/> of the argument determining whether the unit includes a bias term. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BiasTerm { get; }
}
