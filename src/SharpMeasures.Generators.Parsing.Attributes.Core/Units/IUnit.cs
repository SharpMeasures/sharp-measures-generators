namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IUnit
{
    /// <summary>The <see cref="ITypeSymbol"/> of the scalar quantity primarily described by the unit.</summary>
    public abstract ITypeSymbol ScalarQuantity { get; }

    /// <summary>Indicates whether the unit includes a bias term.</summary>
    public abstract bool? BiasTerm { get; }
}
