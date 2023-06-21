namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ExcludeUnitBasesAttribute"/> to be parsed.</summary>
public sealed class ExcludeUnitBasesParser : ISyntacticExcludeUnitBasesParser, ISemanticExcludeUnitBasesParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitBasesParser"/>, parsing the arguments of a <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ExcludeUnitBasesParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticExcludeUnitBases? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ExcludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IExcludeUnitBases? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ExcludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticExcludeUnitBases CreateSyntactic(ExcludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new SyntacticExcludeUnitBases(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IExcludeUnitBases CreateSemantic(ExcludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new SemanticExcludeUnitBases(recorder.UnitInstances);
    }

    private static IExcludeUnitBasesSyntax CreateSyntax(ExcludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new ExcludeUnitBasesSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitInstancesCollectionLocation, recorder.UnitInstancesElementLocations);
    }

    private sealed class ExcludeUnitBasesAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public IReadOnlyList<string?>? UnitInstances { get; private set; }

        public Location UnitInstancesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> UnitInstancesElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("UnitInstances", Adapters.ForNullable<string>(RecordUnitInstances));
        }

        private void RecordUnitInstances(IReadOnlyList<string?>? unitInstances, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            UnitInstances = unitInstances;
            UnitInstancesCollectionLocation = collectionLocation;
            UnitInstancesElementLocations = elementLocations;
        }
    }

    private sealed class SyntacticExcludeUnitBases : ISyntacticExcludeUnitBases
    {
        private IExcludeUnitBases Semantics { get; }
        private IExcludeUnitBasesSyntax Syntax { get; }

        public SyntacticExcludeUnitBases(IExcludeUnitBases semantics, IExcludeUnitBasesSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<string?>? IExcludeUnitBases.UnitInstances => Semantics.UnitInstances;

        IExcludeUnitBasesSyntax ISyntacticExcludeUnitBases.Syntax => Syntax;
    }

    private sealed class SemanticExcludeUnitBases : IExcludeUnitBases
    {
        private IReadOnlyList<string?>? UnitInstances { get; }

        public SemanticExcludeUnitBases(IReadOnlyList<string?>? unitInstances)
        {
            UnitInstances = unitInstances;
        }

        IReadOnlyList<string?>? IExcludeUnitBases.UnitInstances => UnitInstances;
    }

    private sealed class ExcludeUnitBasesSyntax : AAttributeSyntax, IExcludeUnitBasesSyntax
    {
        private Location UnitInstancesCollection { get; }
        private IReadOnlyList<Location> UnitInstancesElements { get; }

        public ExcludeUnitBasesSyntax(Location attributeName, Location attribute, Location unitInstancesCollection, IReadOnlyList<Location> unitInstancesElements)
            : base(attributeName, attribute)
        {
            UnitInstancesCollection = unitInstancesCollection;
            UnitInstancesElements = unitInstancesElements;
        }

        Location IExcludeUnitBasesSyntax.UnitInstancesCollection => UnitInstancesCollection;
        IReadOnlyList<Location> IExcludeUnitBasesSyntax.UnitInstancesElements => UnitInstancesElements;
    }
}
