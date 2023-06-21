namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="IncludeUnitInstancesAttribute"/> to be parsed.</summary>
public sealed class IncludeUnitInstancesParser : ISyntacticIncludeUnitInstancesParser, ISemanticIncludeUnitInstancesParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitInstancesParser"/>, parsing the arguments of a <see cref="IncludeUnitInstancesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public IncludeUnitInstancesParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticIncludeUnitInstances? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        IncludeUnitInstancesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IIncludeUnitInstances? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        IncludeUnitInstancesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticIncludeUnitInstances CreateSyntactic(IncludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new SyntacticIncludeUnitInstances(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IIncludeUnitInstances CreateSemantic(IncludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new SemanticIncludeUnitInstances(recorder.UnitInstances);
    }

    private static IIncludeUnitInstancesSyntax CreateSyntax(IncludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new IncludeUnitInstancesSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitInstancesCollectionLocation, recorder.UnitInstancesElementLocations);
    }

    private sealed class IncludeUnitInstancesAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticIncludeUnitInstances : ISyntacticIncludeUnitInstances
    {
        private IIncludeUnitInstances Semantics { get; }
        private IIncludeUnitInstancesSyntax Syntax { get; }

        public SyntacticIncludeUnitInstances(IIncludeUnitInstances semantics, IIncludeUnitInstancesSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<string?>? IIncludeUnitInstances.UnitInstances => Semantics.UnitInstances;

        IIncludeUnitInstancesSyntax ISyntacticIncludeUnitInstances.Syntax => Syntax;
    }

    private sealed class SemanticIncludeUnitInstances : IIncludeUnitInstances
    {
        private IReadOnlyList<string?>? UnitInstances { get; }

        public SemanticIncludeUnitInstances(IReadOnlyList<string?>? unitInstances)
        {
            UnitInstances = unitInstances;
        }

        IReadOnlyList<string?>? IIncludeUnitInstances.UnitInstances => UnitInstances;
    }

    private sealed class IncludeUnitInstancesSyntax : AAttributeSyntax, IIncludeUnitInstancesSyntax
    {
        private Location UnitInstancesCollection { get; }
        private IReadOnlyList<Location> UnitInstancesElements { get; }

        public IncludeUnitInstancesSyntax(Location attributeName, Location attribute, Location unitInstancesCollation, IReadOnlyList<Location> unitInstancesElements)
            : base(attributeName, attribute)
        {
            UnitInstancesCollection = unitInstancesCollation;
            UnitInstancesElements = unitInstancesElements;
        }

        Location IIncludeUnitInstancesSyntax.UnitInstancesCollection => UnitInstancesCollection;
        IReadOnlyList<Location> IIncludeUnitInstancesSyntax.UnitInstancesElements => UnitInstancesElements;
    }
}
