﻿namespace SharpMeasures.Generators.Attributes.Parsing.DocumentationCases.EnableDocumentationRecordFactoryCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Documentation;
using SharpMeasures.Generators.Attributes.Parsing.Documentation;

using System;

using Xunit;

public sealed class Create
{
    private static IEnableDocumentationRecord Target(IEnableDocumentationRecordFactory factory, AttributeSyntax attributeSyntax) => factory.Create(attributeSyntax);

    private FactoryContext Context { get; } = FactoryContext.Create();

    [Fact]
    public void NullAttributeSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidAttributeSyntax_RecordContainsAttributeSyntax()
    {
        var attributeSyntax = AttributeSyntaxFactory.Create();

        var actual = Target(Context.Factory, attributeSyntax);

        Assert.Equal(actual.Syntactic.Attribute, attributeSyntax);
    }
}
