namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitInstanceAliasAttribute"/> to be parsed.</summary>
public sealed class AliasedUnitInstanceParser : ISyntacticAliasedUnitInstanceParser, ISemanticAliasedUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="AliasedUnitInstanceParser"/>, parsing the arguments of a <see cref="UnitInstanceAliasAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public AliasedUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticAliasedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        AliasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IAliasedUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        AliasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticAliasedUnitInstance CreateSyntactic(AliasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SyntacticAliasedUnitInstance(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private IAliasedUnitInstance CreateSemantic(AliasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new SemanticAliasedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance);
    }

    private IAliasedUnitInstanceSyntax CreateSyntax(AliasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new AliasedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation);
    }

    private sealed class AliasedUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("AliasOf", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
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

        private void RecordOriginalUnitInstance(string? originalUnitInstance, Location location)
        {
            OriginalUnitInstance = originalUnitInstance;
            OriginalUnitInstanceLocation = location;
        }
    }

    private sealed class SyntacticAliasedUnitInstance : ASyntacticModifiedUnitInstance<IAliasedUnitInstance, IAliasedUnitInstanceSyntax>, ISyntacticAliasedUnitInstance
    {
        public SyntacticAliasedUnitInstance(IAliasedUnitInstance semantics, IAliasedUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        IAliasedUnitInstanceSyntax ISyntacticAliasedUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticAliasedUnitInstance : ASemanticModifiedUnitInstance, IAliasedUnitInstance
    {
        public SemanticAliasedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance) : base(name, pluralForm, originalUnitInstance) { }
    }

    private sealed class AliasedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IAliasedUnitInstanceSyntax
    {
        public AliasedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm, Location originalUnitInstance) : base(attributeName, attribute, name, pluralForm, originalUnitInstance) { }
    }
}
