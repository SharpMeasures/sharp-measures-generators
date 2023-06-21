namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedUnitlessQuantityParser : ISyntacticSpecializedUnitlessQuantityParser, ISemanticSpecializedUnitlessQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantityParser"/>, parsing the arguments of a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedUnitlessQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticSpecializedUnitlessQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedUnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public ISpecializedUnitlessQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        SpecializedUnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticSpecializedUnitlessQuantity? CreateSyntactic(SpecializedUnitlessQuantityAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not ISpecializedUnitlessQuantity semantics)
        {
            return null;
        }

        return new SyntacticSpecializedUnitlessQuantity(semantics, CreateSyntax(recorder));
    }

    private static ISpecializedUnitlessQuantity? CreateSemantic(SpecializedUnitlessQuantityAttributeArgumentRecorder recorder)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new SemanticSpecializedUnitlessQuantity(recorder.Original);
    }

    private static ISpecializedUnitlessQuantitySyntax CreateSyntax(SpecializedUnitlessQuantityAttributeArgumentRecorder recorder)
    {
        return new SpecializedUnitlessQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.OriginalLocation);
    }

    private sealed class SpecializedUnitlessQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticSpecializedUnitlessQuantity : ISyntacticSpecializedUnitlessQuantity
    {
        private ISpecializedUnitlessQuantity Semantics { get; }
        private ISpecializedUnitlessQuantitySyntax Syntax { get; }

        public SyntacticSpecializedUnitlessQuantity(ISpecializedUnitlessQuantity semantics, ISpecializedUnitlessQuantitySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol ISpecializedUnitlessQuantity.Original => Semantics.Original;

        ISpecializedUnitlessQuantitySyntax ISyntacticSpecializedUnitlessQuantity.Syntax => Syntax;
    }

    private sealed class SemanticSpecializedUnitlessQuantity : ISpecializedUnitlessQuantity
    {
        private ITypeSymbol Original { get; }

        public SemanticSpecializedUnitlessQuantity(ITypeSymbol original)
        {
            Original = original;
        }

        ITypeSymbol ISpecializedUnitlessQuantity.Original => Original;
    }

    private sealed class SpecializedUnitlessQuantitySyntax : AAttributeSyntax, ISpecializedUnitlessQuantitySyntax
    {
        private Location Original { get; }

        public SpecializedUnitlessQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }

        Location ISpecializedUnitlessQuantitySyntax.Original => Original;
    }
}
