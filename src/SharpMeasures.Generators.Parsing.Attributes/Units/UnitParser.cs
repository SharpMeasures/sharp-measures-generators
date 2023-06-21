namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitAttribute{TScalar}"/> to be parsed.</summary>
public sealed class UnitParser : ISyntacticUnitParser, ISemanticUnitParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitParser"/>, parsing the arguments of a <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticUnit? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        UnitAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IUnit? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        UnitAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticUnit? CreateSyntactic(UnitAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IUnit semantics)
        {
            return null;
        }

        return new SyntacticUnit(semantics, CreateSyntax(recorder));
    }

    private IUnit? CreateSemantic(UnitAttributeArgumentRecorder recorder)
    {
        if (recorder.ScalarQuantity is null)
        {
            return null;
        }

        return new SemanticUnit(recorder.ScalarQuantity, recorder.BiasTerm);
    }

    private IUnitSyntax CreateSyntax(UnitAttributeArgumentRecorder recorder)
    {
        return new UnitSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.ScalarQuantityLocation, recorder.BiasTermLocation);
    }

    private sealed class UnitAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? ScalarQuantity { get; private set; }
        public bool? BiasTerm { get; private set; }

        public Location ScalarQuantityLocation { get; private set; } = Location.None;
        public Location BiasTermLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TScalar", Adapters.For(RecordScalarQuantity));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("BiasTerm", Adapters.For<bool>(RecordBiasTerm));
        }

        private void RecordScalarQuantity(ITypeSymbol scalarQuantity, Location location)
        {
            ScalarQuantity = scalarQuantity;
            ScalarQuantityLocation = location;
        }

        private void RecordBiasTerm(bool biasTerm, Location location)
        {
            BiasTerm = biasTerm;
            BiasTermLocation = location;
        }
    }

    private sealed class SyntacticUnit : ISyntacticUnit
    {
        private IUnit SemanticUnit { get; }
        private IUnitSyntax Syntax { get; }

        public SyntacticUnit(IUnit semantics, IUnitSyntax syntax)
        {
            SemanticUnit = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IUnit.ScalarQuantity => SemanticUnit.ScalarQuantity;
        bool? IUnit.BiasTerm => SemanticUnit.BiasTerm;

        IUnitSyntax ISyntacticUnit.Syntax => Syntax;
    }

    private sealed class SemanticUnit : IUnit
    {
        private ITypeSymbol ScalarQuantity { get; }
        private bool? BiasTerm { get; }

        public SemanticUnit(ITypeSymbol scalarQuantity, bool? biasTerm)
        {
            ScalarQuantity = scalarQuantity;
            BiasTerm = biasTerm;
        }

        ITypeSymbol IUnit.ScalarQuantity => ScalarQuantity;
        bool? IUnit.BiasTerm => BiasTerm;
    }

    private sealed class UnitSyntax : AAttributeSyntax, IUnitSyntax
    {
        private Location ScalarQuantity { get; }
        private Location BiasTerm { get; }

        public UnitSyntax(Location attributeName, Location attribute, Location scalarQuantity, Location biasTerm) : base(attributeName, attribute)
        {
            ScalarQuantity = scalarQuantity;
            BiasTerm = biasTerm;
        }

        Location IUnitSyntax.ScalarQuantity => ScalarQuantity;
        Location IUnitSyntax.BiasTerm => BiasTerm;
    }
}
