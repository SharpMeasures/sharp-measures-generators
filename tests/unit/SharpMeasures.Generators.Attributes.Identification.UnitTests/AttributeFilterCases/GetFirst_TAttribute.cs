namespace SharpMeasures.Generators.Attributes.Identification.AttributeFilterCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

public sealed class GetFirst_TAttribute
{
    private static AttributeData? Target<TAttribute>(IAttributeFilter filter, IEnumerable<AttributeData> attributes) where TAttribute : Attribute => filter.GetFirst<TAttribute>(attributes);

    private FilterContext Context { get; } = FilterContext.Create();

    [Fact]
    public void NullAttributes_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target<FactAttribute>(Context.Filter, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullContainingAttributes_ArgumentException()
    {
        var exception = Record.Exception(() => Target<FactAttribute>(Context.Filter, new AttributeData[] { null! }));

        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void EmptyAttributes_ReturnsNull()
    {
        var actual = Target<FactAttribute>(Context.Filter, Enumerable.Empty<AttributeData>());

        Assert.Null(actual);
    }

    [Fact]
    public void FalseReturningIdentifier_ReturnsNull()
    {
        var attributes = new[] { Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup(static (identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), It.IsAny<AttributeData>())).Returns(false);

        var actual = Target<FactAttribute>(Context.Filter, attributes);

        Assert.Null(actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[0]), Times.Once);
    }

    [Fact]
    public void TrueReturningIdentifier_ReturnsAttribute()
    {
        var attributes = new[] { Mock.Of<AttributeData>(), Mock.Of<AttributeData>() };

        Context.IdentifierMock.Setup((identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), attributes[0])).Returns(false);
        Context.IdentifierMock.Setup((identifier) => identifier.IsOfAttributeClass(It.IsAny<Type>(), attributes[1])).Returns(true);

        var actual = Target<FactAttribute>(Context.Filter, attributes);

        Assert.Equal(attributes[1], actual);

        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[0]), Times.Once);
        Context.IdentifierMock.Verify((identifier) => identifier.IsOfAttributeClass(typeof(FactAttribute), attributes[1]), Times.Once);
    }
}
