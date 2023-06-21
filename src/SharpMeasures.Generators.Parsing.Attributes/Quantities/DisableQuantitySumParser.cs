namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;

/// <summary>Allows the arguments of a <see cref="DisableQuantitySumAttribute"/> to be parsed.</summary>
public sealed class DisableQuantitySumParser : ISyntacticDisableQuantitySumParser, ISemanticDisableQuantitySumParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DisableQuantitySumParser"/>, parsing the arguments of a <see cref="DisableQuantitySumAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DisableQuantitySumParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticDisableQuantitySum? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DisableQuantitySumArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IDisableQuantitySum? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        DisableQuantitySumArgumentRecorder recoder = new();

        if (SemanticParser.TryParse(recoder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic();
    }

    private static ISyntacticDisableQuantitySum CreateSyntactic(DisableQuantitySumArgumentRecorder recorder)
    {
        return new SyntacticDisableQuantitySum(CreateSyntax(recorder));
    }

    private static IDisableQuantitySum CreateSemantic()
    {
        return new SemanticDisableQuantitySum();
    }

    private static IDisableQuantitySumSyntax CreateSyntax(DisableQuantitySumArgumentRecorder recorder)
    {
        return new DisableQuantitySumSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation);
    }

    private sealed class DisableQuantitySumArgumentRecorder : Attributes.AArgumentRecorder { }

    private sealed class SyntacticDisableQuantitySum : ISyntacticDisableQuantitySum
    {
        private IDisableQuantitySumSyntax Syntax { get; }

        public SyntacticDisableQuantitySum(IDisableQuantitySumSyntax syntax)
        {
            Syntax = syntax;
        }

        IDisableQuantitySumSyntax ISyntacticDisableQuantitySum.Syntax => Syntax;
    }

    private sealed class SemanticDisableQuantitySum : IDisableQuantitySum { }

    private sealed class DisableQuantitySumSyntax : AAttributeSyntax, IDisableQuantitySumSyntax
    {
        public DisableQuantitySumSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
