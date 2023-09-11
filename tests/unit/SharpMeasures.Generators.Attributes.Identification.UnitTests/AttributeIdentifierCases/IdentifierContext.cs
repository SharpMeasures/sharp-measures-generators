namespace SharpMeasures.Generators.Attributes.Identification.AttributeIdentifierCases;

internal sealed class IdentifierContext
{
    public static IdentifierContext Create()
    {
        AttributeIdentifier filter = new();

        return new(filter);
    }

    public AttributeIdentifier Identifier { get; }

    private IdentifierContext(AttributeIdentifier filter)
    {
        Identifier = filter;
    }
}
