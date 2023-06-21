namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantitySumAttribute{TSum}"/> to be parsed.</summary>
public sealed class QuantitySumParser : ISyntacticQuantitySumParser, ISemanticQuantitySumParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantitySumParser"/>, parsing the arguments of a <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantitySumParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticQuantitySum? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantitySumAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IQuantitySum? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        QuantitySumAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticQuantitySum? CreateSyntactic(QuantitySumAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IQuantitySum semantics)
        {
            return null;
        }

        return new SyntacticQuantitySum(semantics, CreateSyntax(recorder));
    }

    private static IQuantitySum? CreateSemantic(QuantitySumAttributeArgumentRecorder recorder)
    {
        if (recorder.Sum is null)
        {
            return null;
        }

        return new SemanticQuantitySum(recorder.Sum);
    }

    private static IQuantitySumSyntax CreateSyntax(QuantitySumAttributeArgumentRecorder recorder)
    {
        return new QuantitySumSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.SumLocation);
    }

    private sealed class QuantitySumAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Sum { get; private set; }

        public Location SumLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TSum", Adapters.For(RecordSum));
        }

        private void RecordSum(ITypeSymbol sum, Location location)
        {
            Sum = sum;
            SumLocation = location;
        }
    }

    private sealed class SyntacticQuantitySum : ISyntacticQuantitySum
    {
        private IQuantitySum Semantics { get; }
        private IQuantitySumSyntax Syntax { get; }

        public SyntacticQuantitySum(IQuantitySum semantics, IQuantitySumSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IQuantitySum.Sum => Semantics.Sum;

        IQuantitySumSyntax ISyntacticQuantitySum.Syntax => Syntax;
    }

    private sealed class SemanticQuantitySum : IQuantitySum
    {
        private ITypeSymbol Sum { get; }

        public SemanticQuantitySum(ITypeSymbol sum)
        {
            Sum = sum;
        }

        ITypeSymbol IQuantitySum.Sum => Sum;
    }

    private sealed class QuantitySumSyntax : AAttributeSyntax, IQuantitySumSyntax
    {
        private Location Sum { get; }

        public QuantitySumSyntax(Location attributeName, Location attribute, Location sum) : base(attributeName, attribute)
        {
            Sum = sum;
        }

        Location IQuantitySumSyntax.Sum => Sum;
    }
}
