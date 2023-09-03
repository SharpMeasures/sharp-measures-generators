namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents the arguments of a <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface ISemanticScalarQuantityRecord
{
    /// <summary>The unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>Indicates whether the quantity is biased.</summary>
    public abstract OneOf<None, bool> Biased { get; }
}
