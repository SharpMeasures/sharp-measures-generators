namespace SharpMeasures.Generators.Attributes.Identification.AttributeFilterCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

public sealed class GetAll_Type
{
    private static IEnumerable<AttributeData> Target(IAttributeFilter filter, Type attributeClass, IEnumerable<AttributeData> attributes) => filter.GetAll(attributeClass, attributes);

    private FilterContext Context { get; } = FilterContext.Create();

    [Fact]
    public void NullAttributeClass_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Filter, null!, Mock.Of<IEnumerable<AttributeData>>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullAttributes_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Filter, Mock.Of<Type>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullContainingAttributes_InvalidOperationExceptionWhenEnumerated()
    {
        var enumerable = Target(Context.Filter, Mock.Of<Type>(), new AttributeData[] { null! });

        var exception = Record.Exception(() => enumerable.All(static (attribute) => true));

        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void EmptyAttributes_ReturnsEmpty()
    {
        var actual = Target(Context.Filter, Mock.Of<Type>(), Enumerable.Empty<AttributeData>());

        Assert.Empty(actual);
    }

    [Fact]
    public void FalseReturningIdentifier_ReturnsEmpty()
    {
        var attributeClass = Mock.Of<Type>();
        var attributes = new[] { Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(false);

        var actual = Target(Context.Filter, attributeClass, attributes);

        Assert.Empty(actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[0]), Times.Once);
    }

    [Fact]
    public void TrueReturningIdentifier_ReturnsAll()
    {
        var attributeClass = Mock.Of<Type>();
        var attributes = new[] { Mock.Of<AttributeData>(), Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(true);

        var actual = Target(Context.Filter, attributeClass, attributes);

        Assert.Equal(attributes, actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[0]), Times.Once);
        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[1]), Times.Once);
    }
}
