namespace SharpMeasures.Generators.Attributes.Identification.AttributeFilterCases;

using Moq;

internal sealed class FilterContext
{
    public static FilterContext Create()
    {
        Mock<IAttributeIdentifier> identifierMock = new();

        AttributeFilter filter = new(identifierMock.Object);

        return new(filter, identifierMock);
    }

    public AttributeFilter Filter { get; }

    public Mock<IAttributeIdentifier> IdentifierMock { get; }

    private FilterContext(AttributeFilter filter, Mock<IAttributeIdentifier> identifierMock)
    {
        Filter = filter;

        IdentifierMock = identifierMock;
    }
}
