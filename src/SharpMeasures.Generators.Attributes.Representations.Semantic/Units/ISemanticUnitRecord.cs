namespace SharpMeasures.Generators.Attributes.Units;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface ISemanticUnitRecord
{
    /// <summary>The scalar quantity primarily described by the unit.</summary>
    public abstract ITypeSymbol ScalarQuantity { get; }

    /// <summary>Indicates whether the unit includes a bias term.</summary>
    public abstract OneOf<None, bool> BiasTerm { get; }
}
