namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedScalarQuantityParser : ISyntacticSpecializedScalarQuantityParser, ISemanticSpecializedScalarQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedScalarQuantityParser"/>, parsing the arguments of a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedScalarQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticSpecializedScalarQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public ISpecializedScalarQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        SpecializedScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticSpecializedScalarQuantity? CreateSyntactic(SpecializedScalarQuantityAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not ISpecializedScalarQuantity semantics)
        {
            return null;
        }

        return new SyntacticSpecializedScalarQuantity(semantics, CreateSyntax(recorder));
    }

    private static ISpecializedScalarQuantity? CreateSemantic(SpecializedScalarQuantityAttributeArgumentRecorder recorder)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new SemanticSpecializedScalarQuantity(recorder.Original);
    }

    private static ISpecializedScalarQuantitySyntax CreateSyntax(SpecializedScalarQuantityAttributeArgumentRecorder recorder)
    {
        return new SpecializedScalarQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.OriginalLocation);
    }

    private sealed class SpecializedScalarQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Original { get; private set; }

        public Location OriginalLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TOriginal", Adapters.For(RecordOriginal));
        }

        private void RecordOriginal(ITypeSymbol original, Location location)
        {
            Original = original;
            OriginalLocation = location;
        }
    }

    private sealed class SyntacticSpecializedScalarQuantity : ISyntacticSpecializedScalarQuantity
    {
        private ISpecializedScalarQuantity Semantics { get; }
        private ISpecializedScalarQuantitySyntax Syntax { get; }

        public SyntacticSpecializedScalarQuantity(ISpecializedScalarQuantity semantics, ISpecializedScalarQuantitySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol ISpecializedScalarQuantity.Original => Semantics.Original;

        ISpecializedScalarQuantitySyntax ISyntacticSpecializedScalarQuantity.Syntax => Syntax;
    }

    private sealed class SemanticSpecializedScalarQuantity : ISpecializedScalarQuantity
    {
        private ITypeSymbol Original { get; }

        public SemanticSpecializedScalarQuantity(ITypeSymbol original)
        {
            Original = original;
        }

        ITypeSymbol ISpecializedScalarQuantity.Original => Original;
    }

    private sealed class SpecializedScalarQuantitySyntax : AAttributeSyntax, ISpecializedScalarQuantitySyntax
    {
        private Location Original { get; }

        public SpecializedScalarQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }

        Location ISpecializedScalarQuantitySyntax.Original => Original;
    }
}
