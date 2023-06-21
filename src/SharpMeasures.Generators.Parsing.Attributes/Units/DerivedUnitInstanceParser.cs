namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DerivedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class DerivedUnitInstanceParser : ISyntacticDerivedUnitInstanceParser, ISemanticDerivedUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DerivedUnitInstanceParser"/>, parsing the arguments of a <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DerivedUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticDerivedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DerivedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IDerivedUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        DerivedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticDerivedUnitInstance CreateSyntactic(DerivedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SyntacticDerivedUnitInstance(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private IDerivedUnitInstance CreateSemantic(DerivedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SemanticDerivedUnitInstance(recorder.Name, recorder.PluralForm, recorder.DerivationID, recorder.Units);
    }

    private IDerivedUnitInstanceSyntax CreateSyntax(DerivedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new DerivedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.DerivationIDLocation, recorder.UnitsCollectionLocation, recorder.UnitsElementLocations);
    }

    private sealed class DerivedUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? DerivationID { get; private set; }
        public IReadOnlyList<string?>? Units { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location DerivationIDLocation { get; private set; } = Location.None;
        public Location UnitsCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> UnitsElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("DerivationID", Adapters.ForNullable<string>(RecordDerivationID));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Units", Adapters.ForNullable<string>(RecordUnits));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordPluralForm(string? pluralForm, Location location)
        {
            if (pluralForm is not null)
            {
                PluralForm = pluralForm;
            }

            PluralFormLocation = location;
        }

        private void RecordDerivationID(string? derivationID, Location location)
        {
            if (derivationID is not null)
            {
                DerivationID = derivationID;
            }

            DerivationIDLocation = location;
        }

        private void RecordUnits(IReadOnlyList<string?>? units, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Units = units;
            UnitsCollectionLocation = collectionLocation;
            UnitsElementLocations = elementLocations;
        }
    }

    private sealed class SyntacticDerivedUnitInstance : ASyntacticUnitInstance<IDerivedUnitInstance, IDerivedUnitInstanceSyntax>, ISyntacticDerivedUnitInstance
    {
        public SyntacticDerivedUnitInstance(IDerivedUnitInstance semantics, IDerivedUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        string? IDerivedUnitInstance.DerivationID => Semantics.DerivationID;
        IReadOnlyList<string?>? IDerivedUnitInstance.Units => Semantics.Units;

        IDerivedUnitInstanceSyntax ISyntacticDerivedUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticDerivedUnitInstance : ASemanticUnitInstance, IDerivedUnitInstance
    {
        private string? DerivationID { get; }
        private IReadOnlyList<string?>? Units { get; }

        public SemanticDerivedUnitInstance(string? name, string? pluralForm, string? derivationID, IReadOnlyList<string?>? units) : base(name, pluralForm)
        {
            DerivationID = derivationID;
            Units = units;
        }

        string? IDerivedUnitInstance.DerivationID => DerivationID;
        IReadOnlyList<string?>? IDerivedUnitInstance.Units => Units;
    }

    private sealed class DerivedUnitInstanceSyntax : AUnitInstanceSyntax, IDerivedUnitInstanceSyntax
    {
        private Location DerivationID { get; }
        private Location UnitsCollection { get; }
        private IReadOnlyList<Location> UnitsElements { get; }

        public DerivedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm, Location derivationID, Location unitsCollection, IReadOnlyList<Location> unitsElements) : base(attributeName, attribute, name, pluralForm)
        {
            DerivationID = derivationID;

            UnitsCollection = unitsCollection;
            UnitsElements = unitsElements;
        }

        Location IDerivedUnitInstanceSyntax.DerivationID => DerivationID;
        Location IDerivedUnitInstanceSyntax.UnitsCollection => UnitsCollection;
        IReadOnlyList<Location> IDerivedUnitInstanceSyntax.UnitsElements => UnitsElements;
    }
}
