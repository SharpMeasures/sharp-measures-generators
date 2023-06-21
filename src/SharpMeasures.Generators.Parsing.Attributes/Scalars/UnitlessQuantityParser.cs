namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;

/// <summary>Allows the arguments of a <see cref="UnitlessQuantityAttribute"/> to be parsed.</summary>
public sealed class UnitlessQuantityParser : ISyntacticUnitlessQuantityParser, ISemanticUnitlessQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitlessQuantityParser"/>, parsing the arguments of a <see cref="UnitlessQuantityAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitlessQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticUnitlessQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        UnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IUnitlessQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        UnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic();
    }

    private static ISyntacticUnitlessQuantity CreateSyntactic(UnitlessQuantityAttributeArgumentRecorder recorder)
    {
        return new SyntacticUnitlessQuantity(CreateSyntax(recorder));
    }

    private static IUnitlessQuantity CreateSemantic()
    {
        return new SemanticUnitlessQuantity();
    }

    private static IUnitlessQuantitySyntax CreateSyntax(UnitlessQuantityAttributeArgumentRecorder recorder)
    {
        return new UnitlessQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation);
    }

    private sealed class UnitlessQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder { }

    private sealed class SyntacticUnitlessQuantity : ISyntacticUnitlessQuantity
    {
        private IUnitlessQuantitySyntax Syntax { get; }

        public SyntacticUnitlessQuantity(IUnitlessQuantitySyntax syntax)
        {
            Syntax = syntax;
        }

        IUnitlessQuantitySyntax ISyntacticUnitlessQuantity.Syntax => Syntax;
    }

    private sealed class SemanticUnitlessQuantity : IUnitlessQuantity { }

    private sealed class UnitlessQuantitySyntax : AAttributeSyntax, IUnitlessQuantitySyntax
    {
        public UnitlessQuantitySyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
