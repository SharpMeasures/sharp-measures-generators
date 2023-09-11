namespace SharpMeasures.Generators.Attributes.Identification.AttributeIdentifierCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class IsOfAttributeClass_TAttribute
{
    private static bool Target<TAttribute>(IAttributeIdentifier identifier, AttributeData attribute) where TAttribute : Attribute => identifier.IsOfAttributeClass<TAttribute>(attribute);

    private IdentifierContext Context { get; } = IdentifierContext.Create();

    [Fact]
    public void NullAttributes_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target<FactAttribute>(Context.Identifier, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullAttributeClass_False()
    {
        var actual = Target<FactAttribute>(Context.Identifier, Mock.Of<AttributeData>());

        Assert.False(actual);
    }

    [Fact]
    public async Task WrongAttributeClass_False()
    {
        var source = """
            [Xunit.Theory]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target<FactAttribute>(Context.Identifier, attributeData);

        Assert.False(actual);
    }

    [Fact]
    public async Task MatchingAttributeClass_True()
    {
        var source = """
            [Xunit.Fact]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target<FactAttribute>(Context.Identifier, attributeData);

        Assert.True(actual);
    }

    [Fact]
    public async Task MatchingGenericAttributeClass_True()
    {
        var source = """
            [GenericAttribute<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target<GenericAttribute<int>>(Context.Identifier, attributeData);

        Assert.True(actual);
    }

    [Fact]
    public async Task MatchingGenericAttributeClassButWrongTypeParameter_False()
    {
        var source = """
            [GenericAttribute<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target<GenericAttribute<string>>(Context.Identifier, attributeData);

        Assert.False(actual);
    }

    [Fact]
    public async Task WrongGenericAttributeArity_False()
    {
        var source = """
            [GenericAttribute<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target<GenericAttribute<int, string>>(Context.Identifier, attributeData);

        Assert.False(actual);
    }
}
