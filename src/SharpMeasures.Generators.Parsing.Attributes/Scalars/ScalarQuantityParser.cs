namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarQuantityAttribute{TUnit}"/> to be parsed.</summary>
public sealed class ScalarQuantityParser : ISyntacticScalarQuantityParser, ISemanticScalarQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarQuantityParser"/>, parsing the arguments of a <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticScalarQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IScalarQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticScalarQuantity? CreateSyntactic(ScalarQuantityAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IScalarQuantity semantics)
        {
            return null;
        }

        return new SyntacticScalarQuantity(semantics, CreateSyntax(recorder));
    }

    private static IScalarQuantity? CreateSemantic(ScalarQuantityAttributeArgumentRecorder recorder)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new SemanticScalarQuantity(recorder.Unit, recorder.Biased);
    }

    private static IScalarQuantitySyntax CreateSyntax(ScalarQuantityAttributeArgumentRecorder recorder)
    {
        return new ScalarQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitLocation, recorder.BiasedLocation);
    }

    private sealed class ScalarQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }
        public bool? Biased { get; private set; }

        public Location UnitLocation { get; private set; } = Location.None;
        public Location BiasedLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Biased", Adapters.For<bool>(RecordBiased));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordBiased(bool useUnitBias, Location location)
        {
            Biased = useUnitBias;
            BiasedLocation = location;
        }
    }

    private sealed class SyntacticScalarQuantity : ISyntacticScalarQuantity
    {
        private IScalarQuantity Semantics { get; }
        private IScalarQuantitySyntax Syntax { get; }

        public SyntacticScalarQuantity(IScalarQuantity semantics, IScalarQuantitySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IScalarQuantity.Unit => Semantics.Unit;

        bool? IScalarQuantity.Biased => Semantics.Biased;

        IScalarQuantitySyntax ISyntacticScalarQuantity.Syntax => Syntax;
    }

    private sealed class SemanticScalarQuantity : IScalarQuantity
    {
        private ITypeSymbol Unit { get; }
        private bool? Biased { get; }

        public SemanticScalarQuantity(ITypeSymbol unit, bool? biased)
        {
            Unit = unit;
            Biased = biased;
        }

        ITypeSymbol IScalarQuantity.Unit => Unit;
        bool? IScalarQuantity.Biased => Biased;
    }

    private sealed class ScalarQuantitySyntax : AAttributeSyntax, IScalarQuantitySyntax
    {
        private Location Unit { get; }
        private Location Biased { get; }

        public ScalarQuantitySyntax(Location attributeName, Location attribute, Location unit, Location biased) : base(attributeName, attribute)
        {
            Unit = unit;
            Biased = biased;
        }

        Location IScalarQuantitySyntax.Unit => Unit;
        Location IScalarQuantitySyntax.Biased => Biased;
    }
}
