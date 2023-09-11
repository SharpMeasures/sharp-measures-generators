namespace SharpMeasures.Generators.Attributes.Identification.AttributeFilterCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

public sealed class GetAll_TAttribute
{
    private static IEnumerable<AttributeData> Target<TAttribute>(IAttributeFilter filter, IEnumerable<AttributeData> attributes) where TAttribute : Attribute => filter.GetAll<TAttribute>(attributes);

    private FilterContext Context { get; } = FilterContext.Create();

    [Fact]
    public void NullAttributes_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target<FactAttribute>(Context.Filter, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullContainingAttributes_InvalidOperationExceptionWhenEnumerated()
    {
        var enumerable = Target<FactAttribute>(Context.Filter, new AttributeData[] { null! });

        var exception = Record.Exception(() => enumerable.All(static (attribute) => true));

        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void EmptyAttributes_ReturnsEmpty()
    {
        var actual = Target<FactAttribute>(Context.Filter, Enumerable.Empty<AttributeData>());

        Assert.Empty(actual);
    }

    [Fact]
    public void FalseReturningIdentifier_ReturnsEmpty()
    {
        var attributes = new[] { Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(false);

        var actual = Target<FactAttribute>(Context.Filter, attributes);

        Assert.Empty(actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[0]), Times.Once);
    }

    [Fact]
    public void TrueReturningIdentifier_ReturnsAll()
    {
        var attributes = new[] { Mock.Of<AttributeData>(), Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(true);

        var actual = Target<FactAttribute>(Context.Filter, attributes);

        Assert.Equal(attributes, actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[0]), Times.Once);
        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[1]), Times.Once);
    }
}
