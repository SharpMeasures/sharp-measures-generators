namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
public interface IQuantityProperty
{
    /// <summary>The resulting type of the property.</summary>
    public abstract ITypeSymbol Result { get; }

    /// <summary>The name of the property.</summary>
    public abstract string? Name { get; }

    /// <summary>The expression describing the property.</summary>
    public abstract string? Expression { get; }
}
