namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityDifferenceAttribute{TDifference}"/> to be parsed.</summary>
public sealed class QuantityDifferenceParser : ISyntacticQuantityDifferenceParser, ISemanticQuantityDifferenceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityDifferenceParser"/>, parsing the arguments of a <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityDifferenceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticQuantityDifference? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantityDifferenceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IQuantityDifference? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        QuantityDifferenceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticQuantityDifference? CreateSyntactic(QuantityDifferenceAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IQuantityDifference semantics)
        {
            return null;
        }

        return new SyntacticQuantityDifference(semantics, CreateSyntax(recorder));
    }

    private static IQuantityDifference? CreateSemantic(QuantityDifferenceAttributeArgumentRecorder recorder)
    {
        if (recorder.Difference is null)
        {
            return null;
        }

        return new SemanticQuantityDifference(recorder.Difference);
    }

    private static IQuantityDifferenceSyntax CreateSyntax(QuantityDifferenceAttributeArgumentRecorder recorder)
    {
        return new QuantityDifferenceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.DifferenceLocation);
    }

    private sealed class QuantityDifferenceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Difference { get; private set; }

        public Location DifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TDifference", Adapters.For(RecordDifference));
        }

        private void RecordDifference(ITypeSymbol difference, Location location)
        {
            Difference = difference;
            DifferenceLocation = location;
        }
    }

    private sealed class SyntacticQuantityDifference : ISyntacticQuantityDifference
    {
        private IQuantityDifference Semantics { get; }
        private IQuantityDifferenceSyntax Syntax { get; }

        public SyntacticQuantityDifference(IQuantityDifference semantics, IQuantityDifferenceSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IQuantityDifference.Difference => Semantics.Difference;

        IQuantityDifferenceSyntax ISyntacticQuantityDifference.Syntax => Syntax;
    }

    private sealed class SemanticQuantityDifference : IQuantityDifference
    {
        private ITypeSymbol Difference { get; }

        public SemanticQuantityDifference(ITypeSymbol difference)
        {
            Difference = difference;
        }

        ITypeSymbol IQuantityDifference.Difference => Difference;
    }

    private sealed class QuantityDifferenceSyntax : AAttributeSyntax, IQuantityDifferenceSyntax
    {
        private Location Difference { get; }

        public QuantityDifferenceSyntax(Location attributeName, Location attribute, Location difference) : base(attributeName, attribute)
        {
            Difference = difference;
        }

        Location IQuantityDifferenceSyntax.Difference => Difference;
    }
}
