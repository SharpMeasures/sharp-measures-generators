namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IAttributeSyntax"/>
internal abstract class AAttributeSyntax : IAttributeSyntax
{
    private Location AttributeName { get; }
    private Location Attribute { get; }

    /// <summary>Instantiates a <see cref="AAttributeSyntax"/>, representing syntactical information about a parsed attribute.</summary>
    /// <param name="attributeName"><inheritdoc cref="IAttributeSyntax.AttributeName" path="/summary"/></param>
    /// <param name="attribute"><inheritdoc cref="IAttributeSyntax.Attribute" path="/summary"/></param>
    protected AAttributeSyntax(Location attributeName, Location attribute)
    {
        AttributeName = attributeName;

        Attribute = attribute;
    }

    Location IAttributeSyntax.AttributeName => AttributeName;
    Location IAttributeSyntax.Attribute => Attribute;
}
