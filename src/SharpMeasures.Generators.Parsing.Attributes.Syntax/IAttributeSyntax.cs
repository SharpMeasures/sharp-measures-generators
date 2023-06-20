namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed attribute.</summary>
public interface IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the name of the attribute.</summary>
    public abstract Location AttributeName { get; }

    /// <summary>The <see cref="Location"/> of the entire attribute.</summary>
    public abstract Location Attribute { get; }
}
