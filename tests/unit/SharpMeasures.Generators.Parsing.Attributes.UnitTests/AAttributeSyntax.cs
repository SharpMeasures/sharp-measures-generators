namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;

internal abstract class AAttributeSyntax : IAttributeSyntax
{
    public Location AttributeName { get; }
    public Location Attribute { get; }

    protected AAttributeSyntax(Location attributeName, Location attribute)
    {
        AttributeName = attributeName;
        Attribute = attribute;
    }
}
