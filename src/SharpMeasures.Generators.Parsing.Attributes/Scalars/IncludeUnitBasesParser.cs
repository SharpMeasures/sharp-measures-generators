namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="IncludeUnitBasesAttribute"/> to be parsed.</summary>
public sealed class IncludeUnitBasesParser : ISyntacticIncludeUnitBasesParser, ISemanticIncludeUnitBasesParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitBasesParser"/>, parsing the arguments of a <see cref="IncludeUnitBasesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public IncludeUnitBasesParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticIncludeUnitBases? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        IncludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IIncludeUnitBases? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        IncludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticIncludeUnitBases CreateSyntactic(IncludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new SyntacticIncludeUnitBases(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IIncludeUnitBases CreateSemantic(IncludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new SemanticIncludeUnitBases(recorder.UnitInstances);
    }

    private static IIncludeUnitBasesSyntax CreateSyntax(IncludeUnitBasesAttributeArgumentRecorder recorder)
    {
        return new IncludeUnitBasesSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitInstancesCollectionLocation, recorder.UnitInstancesElementLocations);
    }

    private sealed class IncludeUnitBasesAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticIncludeUnitBases : ISyntacticIncludeUnitBases
    {
        private IIncludeUnitBases Semantics { get; }
        private IIncludeUnitBasesSyntax Syntax { get; }

        public SyntacticIncludeUnitBases(IIncludeUnitBases semantics, IIncludeUnitBasesSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<string?>? IIncludeUnitBases.UnitInstances => Semantics.UnitInstances;

        IIncludeUnitBasesSyntax ISyntacticIncludeUnitBases.Syntax => Syntax;
    }

    private sealed class SemanticIncludeUnitBases : IIncludeUnitBases
    {
        private IReadOnlyList<string?>? UnitInstances { get; }

        public SemanticIncludeUnitBases(IReadOnlyList<string?>? unitInstances)
        {
            UnitInstances = unitInstances;
        }

        IReadOnlyList<string?>? IIncludeUnitBases.UnitInstances => UnitInstances;
    }

    private sealed class IncludeUnitBasesSyntax : AAttributeSyntax, IIncludeUnitBasesSyntax
    {
        private Location UnitInstancesCollection { get; }
        private IReadOnlyList<Location> UnitInstancesElements { get; }

        public IncludeUnitBasesSyntax(Location attributeName, Location attribute, Location unitInstancesCollection, IReadOnlyList<Location> unitInstancesElements)
            : base(attributeName, attribute)
        {
            UnitInstancesCollection = unitInstancesCollection;
            UnitInstancesElements = unitInstancesElements;
        }

        Location IIncludeUnitBasesSyntax.UnitInstancesCollection => UnitInstancesCollection;
        IReadOnlyList<Location> IIncludeUnitBasesSyntax.UnitInstancesElements => UnitInstancesElements;
    }
}
