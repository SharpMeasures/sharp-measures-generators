namespace SharpMeasures.Generators.Attributes.Identification.AttributeIdentifierCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;
using System.Threading.Tasks;

using Xunit;

public sealed class IsOfAttributeClass_Type
{
    private static bool Target(IAttributeIdentifier identifier, Type attributeClass, AttributeData attribute) => identifier.IsOfAttributeClass(attributeClass, attribute);

    private IdentifierContext Context { get; } = IdentifierContext.Create();

    [Fact]
    public void NullAttributeClass_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Identifier, null!, Mock.Of<AttributeData>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullAttributes_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Identifier, Mock.Of<Type>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullAttributeDataAttributeClass_False()
    {
        var actual = Target(Context.Identifier, Mock.Of<Type>(), Mock.Of<AttributeData>());

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

        var actual = Target(Context.Identifier, typeof(FactAttribute), attributeData);

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

        var actual = Target(Context.Identifier, typeof(FactAttribute), attributeData);

        Assert.True(actual);
    }

    [Fact]
    public async Task MatchingClosedGenericAttributeClass_True()
    {
        var source = """
            [GenericAttribute<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target(Context.Identifier, typeof(GenericAttribute<int>), attributeData);

        Assert.True(actual);
    }

    [Fact]
    public async Task MatchingOpenGenericAttributeClass_True()
    {
        var source = """
            [GenericAttribute<int>]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents("Foo", source);

        var actual = Target(Context.Identifier, typeof(GenericAttribute<>), attributeData);

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

        var actual = Target(Context.Identifier, typeof(GenericAttribute<string>), attributeData);

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

        var actual = Target(Context.Identifier, typeof(GenericAttribute<int, string>), attributeData);

        Assert.False(actual);
    }
}
