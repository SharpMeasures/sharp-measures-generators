namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedVectorQuantityParser : ISyntacticSpecializedVectorQuantityParser, ISemanticSpecializedVectorQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorQuantityParser"/>, parsing the arguments of a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedVectorQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticSpecializedVectorQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedVectorQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public ISpecializedVectorQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        SpecializedVectorQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticSpecializedVectorQuantity? CreateSyntactic(SpecializedVectorQuantityAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not ISpecializedVectorQuantity semantics)
        {
            return null;
        }

        return new SyntacticSpecializedVectorQuantity(semantics, CreateSyntax(recorder));
    }

    private static ISpecializedVectorQuantity? CreateSemantic(SpecializedVectorQuantityAttributeArgumentRecorder recorder)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new SemanticSpecializedVectorQuantity(recorder.Original);
    }

    private static ISpecializedVectorQuantitySyntax CreateSyntax(SpecializedVectorQuantityAttributeArgumentRecorder recorder)
    {
        return new SpecializedVectorQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.OriginalLocation);
    }

    private sealed class SpecializedVectorQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticSpecializedVectorQuantity : ISyntacticSpecializedVectorQuantity
    {
        private ISpecializedVectorQuantity Semantics { get; }
        private ISpecializedVectorQuantitySyntax Syntax { get; }

        public SyntacticSpecializedVectorQuantity(ISpecializedVectorQuantity semantics, ISpecializedVectorQuantitySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol ISpecializedVectorQuantity.Original => Semantics.Original;

        ISpecializedVectorQuantitySyntax ISyntacticSpecializedVectorQuantity.Syntax => Syntax;
    }

    private sealed class SemanticSpecializedVectorQuantity : ISpecializedVectorQuantity
    {
        private ITypeSymbol Original { get; }

        public SemanticSpecializedVectorQuantity(ITypeSymbol original)
        {
            Original = original;
        }

        ITypeSymbol ISpecializedVectorQuantity.Original => Original;
    }

    private sealed class SpecializedVectorQuantitySyntax : AAttributeSyntax, ISpecializedVectorQuantitySyntax
    {
        private Location Original { get; }

        public SpecializedVectorQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }

        Location ISpecializedVectorQuantitySyntax.Original => Original;
    }
}
