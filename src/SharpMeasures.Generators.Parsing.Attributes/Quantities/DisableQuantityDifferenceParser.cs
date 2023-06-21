namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;

/// <summary>Allows the arguments of a <see cref="DisableQuantityDifferenceAttribute"/> to be parsed.</summary>
public sealed class DisableQuantityDifferenceParser : ISyntacticDisableQuantityDifferenceParser, ISemanticDisableQuantityDifferenceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DisableQuantityDifferenceParser"/>, parsing the arguments of a <see cref="DisableQuantityDifferenceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DisableQuantityDifferenceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticDisableQuantityDifference? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DisableQuantityDifferenceArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IDisableQuantityDifference? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        DisableQuantityDifferenceArgumentRecorder recoder = new();

        if (SemanticParser.TryParse(recoder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic();
    }

    private static ISyntacticDisableQuantityDifference CreateSyntactic(DisableQuantityDifferenceArgumentRecorder recorder)
    {
        return new SyntacticDisableQuantityDifference(CreateSyntax(recorder));
    }

    private static IDisableQuantityDifference CreateSemantic()
    {
        return new SemanticDisableQuantityDifference();
    }

    private static IDisableQuantityDifferenceSyntax CreateSyntax(DisableQuantityDifferenceArgumentRecorder recorder)
    {
        return new DisableQuantityDifferenceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation);
    }

    private sealed class DisableQuantityDifferenceArgumentRecorder : Attributes.AArgumentRecorder { }

    private sealed class SyntacticDisableQuantityDifference : ISyntacticDisableQuantityDifference
    {
        private IDisableQuantityDifferenceSyntax Syntax { get; }

        public SyntacticDisableQuantityDifference(IDisableQuantityDifferenceSyntax syntax)
        {
            Syntax = syntax;
        }

        IDisableQuantityDifferenceSyntax ISyntacticDisableQuantityDifference.Syntax => Syntax;
    }

    private sealed class SemanticDisableQuantityDifference : IDisableQuantityDifference { }

    private sealed class DisableQuantityDifferenceSyntax : AAttributeSyntax, IDisableQuantityDifferenceSyntax
    {
        public DisableQuantityDifferenceSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
