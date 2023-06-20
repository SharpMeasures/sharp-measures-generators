namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents the syntactical information about a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
public interface IQuantityPropertySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the resulting type of the property.</summary>
    public abstract Location Result { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the property.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the expression describing the property.</summary>
    public abstract Location Expression { get; }
}
