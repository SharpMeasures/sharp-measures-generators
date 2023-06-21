namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedVectorGroupParser : ISyntacticSpecializedVectorGroupParser, ISemanticSpecializedVectorGroupParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorGroupParser"/>, parsing the arguments of a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedVectorGroupParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticSpecializedVectorGroup? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedVectorGroupAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public ISpecializedVectorGroup? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        SpecializedVectorGroupAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticSpecializedVectorGroup? CreateSyntactic(SpecializedVectorGroupAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not ISpecializedVectorGroup semantics)
        {
            return null;
        }

        return new SyntacticSpecializedVectorGroup(semantics, CreateSyntax(recorder));
    }

    private static ISpecializedVectorGroup? CreateSemantic(SpecializedVectorGroupAttributeArgumentRecorder recorder)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new SemanticSpecializedVectorGroup(recorder.Original);
    }

    private static ISpecializedVectorGroupSyntax CreateSyntax(SpecializedVectorGroupAttributeArgumentRecorder recorder)
    {
        return new SpecializedVectorGroupSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.OriginalLocation);
    }

    private sealed class SpecializedVectorGroupAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticSpecializedVectorGroup : ISyntacticSpecializedVectorGroup
    {
        private ISpecializedVectorGroup Semantics { get; }
        private ISpecializedVectorGroupSyntax Syntax { get; }

        public SyntacticSpecializedVectorGroup(ISpecializedVectorGroup semantics, ISpecializedVectorGroupSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol ISpecializedVectorGroup.Original => Semantics.Original;

        ISpecializedVectorGroupSyntax ISyntacticSpecializedVectorGroup.Syntax => Syntax;
    }

    private sealed class SemanticSpecializedVectorGroup : ISpecializedVectorGroup
    {
        private ITypeSymbol Original { get; }

        public SemanticSpecializedVectorGroup(ITypeSymbol original)
        {
            Original = original;
        }

        ITypeSymbol ISpecializedVectorGroup.Original => Original;
    }

    private sealed class SpecializedVectorGroupSyntax : AAttributeSyntax, ISpecializedVectorGroupSyntax
    {
        private Location Original { get; }

        public SpecializedVectorGroupSyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }

        Location ISpecializedVectorGroupSyntax.Original => Original;
    }
}
