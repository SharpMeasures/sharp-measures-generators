namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="FixedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class FixedUnitInstanceParser : ISyntacticFixedUnitInstanceParser, ISemanticFixedUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="FixedUnitInstanceParser"/>, parsing the arguments of a <see cref="FixedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public FixedUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticFixedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        FixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IFixedUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        FixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticFixedUnitInstance CreateSyntactic(FixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SyntacticFixedUnitInstance(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private IFixedUnitInstance CreateSemantic(FixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SemanticFixedUnitInstance(recorder.Name, recorder.PluralForm);
    }

    private IFixedUnitInstanceSyntax CreateSyntax(FixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new FixedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation);
    }

    private sealed class FixedUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
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
    }

    private sealed class SyntacticFixedUnitInstance : ASyntacticUnitInstance<IFixedUnitInstance, IFixedUnitInstanceSyntax>, ISyntacticFixedUnitInstance
    {
        public SyntacticFixedUnitInstance(IFixedUnitInstance semantics, IFixedUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        IFixedUnitInstanceSyntax ISyntacticFixedUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticFixedUnitInstance : ASemanticUnitInstance, IFixedUnitInstance
    {
        public SemanticFixedUnitInstance(string? name, string? pluralForm) : base(name, pluralForm) { }
    }

    private sealed class FixedUnitInstanceSyntax : AUnitInstanceSyntax, IFixedUnitInstanceSyntax
    {
        public FixedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm) : base(attributeName, attribute, name, pluralForm) { }
    }
}
