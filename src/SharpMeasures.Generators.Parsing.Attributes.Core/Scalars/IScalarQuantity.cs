namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface IScalarQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>Dictates whether the quantity is biased.</summary>
    public abstract bool? Biased { get; }
}
