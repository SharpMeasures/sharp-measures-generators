namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorQuantityAttribute{TUnit}"/> to be parsed.</summary>
public sealed class VectorQuantityParser : ISyntacticVectorQuantityParser, ISemanticVectorQuantityParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorQuantityParser"/>, parsing the arguments of a <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorQuantityParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorQuantity? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorQuantity? CreateSyntactic(VectorQuantityAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IVectorQuantity semantics)
        {
            return null;
        }

        return new SyntacticVectorQuantity(semantics, CreateSyntax(recorder));
    }

    private static IVectorQuantity? CreateSemantic(VectorQuantityAttributeArgumentRecorder recorder)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new SemanticVectorQuantity(recorder.Unit, recorder.Dimension);
    }

    private static IVectorQuantitySyntax CreateSyntax(VectorQuantityAttributeArgumentRecorder recorder)
    {
        return new VectorQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitLocation, recorder.DimensionLocation);
    }

    private sealed class VectorQuantityAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }
        public int? Dimension { get; private set; }

        public Location UnitLocation { get; private set; } = Location.None;
        public Location DimensionLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Dimension", Adapters.For<int>(RecordDimension));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordDimension(int dimension, Location location)
        {
            Dimension = dimension;
            DimensionLocation = location;
        }
    }

    private sealed class SyntacticVectorQuantity : ISyntacticVectorQuantity
    {
        private IVectorQuantity Semantics { get; }
        private IVectorQuantitySyntax Syntax { get; }

        public SyntacticVectorQuantity(IVectorQuantity semantics, IVectorQuantitySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IVectorQuantity.Unit => Semantics.Unit;

        int? IVectorQuantity.Dimension => Semantics.Dimension;

        IVectorQuantitySyntax ISyntacticVectorQuantity.Syntax => Syntax;
    }

    private sealed class SemanticVectorQuantity : IVectorQuantity
    {
        private ITypeSymbol Unit { get; }

        private int? Dimension { get; }

        public SemanticVectorQuantity(ITypeSymbol unit, int? dimension)
        {
            Unit = unit;

            Dimension = dimension;
        }

        ITypeSymbol IVectorQuantity.Unit => Unit;

        int? IVectorQuantity.Dimension => Dimension;
    }

    private sealed class VectorQuantitySyntax : AAttributeSyntax, IVectorQuantitySyntax
    {
        private Location Unit { get; }
        private Location Dimension { get; }

        public VectorQuantitySyntax(Location attributeName, Location attribute, Location unit, Location dimension) : base(attributeName, attribute)
        {
            Unit = unit;
            Dimension = dimension;
        }

        Location IVectorQuantitySyntax.Unit => Unit;
        Location IVectorQuantitySyntax.Dimension => Dimension;
    }
}
