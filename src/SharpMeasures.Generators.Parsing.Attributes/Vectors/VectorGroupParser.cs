namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorGroupAttribute{TUnit}"/> to be parsed.</summary>
public sealed class VectorGroupParser : ISyntacticVectorGroupParser, ISemanticVectorGroupParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorGroupParser"/>, parsing the arguments of a <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorGroupParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorGroup? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorGroupAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorGroup? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorGroupAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorGroup? CreateSyntactic(VectorGroupAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IVectorGroup semantics)
        {
            return null;
        }

        return new SyntacticVectorGroup(semantics, CreateSyntax(recorder));
    }

    private static IVectorGroup? CreateSemantic(VectorGroupAttributeArgumentRecorder recorder)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new SemanticVectorGroup(recorder.Unit);
    }

    private static IVectorGroupSyntax CreateSyntax(VectorGroupAttributeArgumentRecorder recorder)
    {
        return new VectorGroupSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitLocation);
    }

    private sealed class VectorGroupAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }

        public Location UnitLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }
    }

    private sealed class SyntacticVectorGroup : ISyntacticVectorGroup
    {
        private IVectorGroup Semantics { get; }
        private IVectorGroupSyntax Syntax { get; }

        public SyntacticVectorGroup(IVectorGroup semantics, IVectorGroupSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IVectorGroup.Unit => Semantics.Unit;

        IVectorGroupSyntax ISyntacticVectorGroup.Syntax => Syntax;
    }

    private sealed class SemanticVectorGroup : IVectorGroup
    {
        private ITypeSymbol Unit { get; }

        public SemanticVectorGroup(ITypeSymbol unit)
        {
            Unit = unit;
        }

        ITypeSymbol IVectorGroup.Unit => Unit;
    }

    private sealed class VectorGroupSyntax : AAttributeSyntax, IVectorGroupSyntax
    {
        private Location Unit { get; }

        public VectorGroupSyntax(Location attributeName, Location attribute, Location unit) : base(attributeName, attribute)
        {
            Unit = unit;
        }

        Location IVectorGroupSyntax.Unit => Unit;
    }
}
