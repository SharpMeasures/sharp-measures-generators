namespace SharpMeasures.Generators.Parsing.Attributes.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="GenerateDocumentationAttribute"/> to be parsed.</summary>
public sealed class GenerateDocumentationParser : ISyntacticGenerateDocumentationParser, ISemanticGenerateDocumentationParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="GenerateDocumentationParser"/>, parsing the arguments of a <see cref="GenerateDocumentationAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public GenerateDocumentationParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticGenerateDocumentation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        GenerateDocumentationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IGenerateDocumentation? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        GenerateDocumentationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticGenerateDocumentation CreateSyntactic(GenerateDocumentationAttributeArgumentRecorder recorder)
    {
        return new SyntacticGenerateDocumentation(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IGenerateDocumentation CreateSemantic(GenerateDocumentationAttributeArgumentRecorder recorder)
    {
        return new SemanticGenerateDocumentation(recorder.Generate);
    }

    private static IGenerateDocumentationSyntax CreateSyntax(GenerateDocumentationAttributeArgumentRecorder recorder)
    {
        return new GenerateDocumentationSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.GenerateLocation);
    }

    private sealed class GenerateDocumentationAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public bool? Generate { get; private set; }

        public Location GenerateLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Generate", Adapters.For<bool>(RecordGenerate));
        }

        private void RecordGenerate(bool generate, Location location)
        {
            Generate = generate;
            GenerateLocation = location;
        }
    }

    private sealed class SyntacticGenerateDocumentation : ISyntacticGenerateDocumentation
    {
        private IGenerateDocumentation Semantics { get; }
        private IGenerateDocumentationSyntax Syntax { get; }

        public SyntacticGenerateDocumentation(IGenerateDocumentation semantics, IGenerateDocumentationSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        bool? IGenerateDocumentation.Generate => Semantics.Generate;

        IGenerateDocumentationSyntax ISyntacticGenerateDocumentation.Syntax => Syntax;
    }

    private sealed class SemanticGenerateDocumentation : IGenerateDocumentation
    {
        private bool? Generate { get; }

        public SemanticGenerateDocumentation(bool? generate)
        {
            Generate = generate;
        }

        bool? IGenerateDocumentation.Generate => Generate;
    }

    private sealed class GenerateDocumentationSyntax : AAttributeSyntax, IGenerateDocumentationSyntax
    {
        private Location Generate { get; }

        public GenerateDocumentationSyntax(Location attributeName, Location attribute, Location generate) : base(attributeName, attribute)
        {
            Generate = generate;
        }

        Location IGenerateDocumentationSyntax.Generate => Generate;
    }
}
