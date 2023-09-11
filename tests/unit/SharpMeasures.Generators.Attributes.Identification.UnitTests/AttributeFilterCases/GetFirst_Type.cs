namespace SharpMeasures.Generators.Attributes.Identification.AttributeFilterCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

public sealed class GetFirst_Type
{
    private static AttributeData? Target(IAttributeFilter filter, Type attributeClass, IEnumerable<AttributeData> attributes) => filter.GetFirst(attributeClass, attributes);

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
    public void NullContainingAttributes_ArgumentException()
    {
        var exception = Record.Exception(() => Target(Context.Filter, Mock.Of<Type>(), new AttributeData[] { null! }));

        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void EmptyAttributes_ReturnsNull()
    {
        var actual = Target(Context.Filter, Mock.Of<Type>(), Enumerable.Empty<AttributeData>());

        Assert.Null(actual);
    }

    [Fact]
    public void FalseReturningIdentifier_ReturnsNull()
    {
        var attributeClass = Mock.Of<Type>();
        var attributes = new[] { Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(false);

        var actual = Target(Context.Filter, attributeClass, attributes);

        Assert.Null(actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[0]), Times.Once);
    }

    [Fact]
    public void TrueReturningIdentifier_ReturnsAttribute()
    {
        var attributeClass = Mock.Of<Type>();
        var attributes = new[] { Mock.Of<AttributeData>(), Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup((identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), attributes[0])).Returns(false);
        Context.IdentifierMock.Setup((identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), attributes[1])).Returns(true);

        var actual = Target(Context.Filter, attributeClass, attributes);

        Assert.Equal(attributes[1], actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[0]), Times.Once);
        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(attributeClass, attributes[1]), Times.Once);
    }
}
