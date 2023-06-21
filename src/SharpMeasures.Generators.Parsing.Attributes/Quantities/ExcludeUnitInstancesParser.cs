namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ExcludeUnitInstancesAttribute"/> to be parsed.</summary>
public sealed class ExcludeUnitInstancesParser : ISyntacticExcludeUnitInstancesParser, ISemanticExcludeUnitInstancesParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitInstancesParser"/>, parsing the arguments of a <see cref="ExcludeUnitInstancesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ExcludeUnitInstancesParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticExcludeUnitInstances? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ExcludeUnitInstancesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IExcludeUnitInstances? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ExcludeUnitInstancesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticExcludeUnitInstances CreateSyntactic(ExcludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new SyntacticExcludeUnitInstances(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IExcludeUnitInstances CreateSemantic(ExcludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new SemanticExcludeUnitInstances(recorder.UnitInstances);
    }

    private static IExcludeUnitInstancesSyntax CreateSyntax(ExcludeUnitInstancesAttributeArgumentRecorder recorder)
    {
        return new ExcludeUnitInstancesSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitInstancesCollectionLocation, recorder.UnitInstancesElementLocations);
    }

    private sealed class ExcludeUnitInstancesAttributeArgumentRecorder : Attributes.AArgumentRecorder
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

    private sealed class SyntacticExcludeUnitInstances : ISyntacticExcludeUnitInstances
    {
        private IExcludeUnitInstances Semantics { get; }
        private IExcludeUnitInstancesSyntax Syntax { get; }

        public SyntacticExcludeUnitInstances(IExcludeUnitInstances semantics, IExcludeUnitInstancesSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<string?>? IExcludeUnitInstances.UnitInstances => Semantics.UnitInstances;

        IExcludeUnitInstancesSyntax ISyntacticExcludeUnitInstances.Syntax => Syntax;
    }

    private sealed class SemanticExcludeUnitInstances : IExcludeUnitInstances
    {
        private IReadOnlyList<string?>? UnitInstances { get; }

        public SemanticExcludeUnitInstances(IReadOnlyList<string?>? unitInstances)
        {
            UnitInstances = unitInstances;
        }

        IReadOnlyList<string?>? IExcludeUnitInstances.UnitInstances => UnitInstances;
    }

    private sealed class ExcludeUnitInstancesSyntax : AAttributeSyntax, IExcludeUnitInstancesSyntax
    {
        private Location UnitInstancesCollection { get; }
        private IReadOnlyList<Location> UnitInstancesElements { get; }

        public ExcludeUnitInstancesSyntax(Location attributeName, Location attribute, Location unitInstancesCollection, IReadOnlyList<Location> unitInstancesElements)
            : base(attributeName, attribute)
        {
            UnitInstancesCollection = unitInstancesCollection;
            UnitInstancesElements = unitInstancesElements;
        }

        Location IExcludeUnitInstancesSyntax.UnitInstancesCollection => UnitInstancesCollection;
        IReadOnlyList<Location> IExcludeUnitInstancesSyntax.UnitInstancesElements => UnitInstancesElements;
    }
}
