namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="PrefixedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class PrefixedUnitInstanceParser : ISyntacticPrefixedUnitInstanceParser, ISemanticPrefixedUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="PrefixedUnitInstanceParser"/>, parsing the arguments of a <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public PrefixedUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticPrefixedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        PrefixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IPrefixedUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        PrefixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticPrefixedUnitInstance? CreateSyntactic(PrefixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IPrefixedUnitInstance semantics)
        {
            return null;
        }

        return new SyntacticPrefixedUnitInstance(semantics, CreateSyntax(recorder));
    }

    private IPrefixedUnitInstance? CreateSemantic(PrefixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (recorder.Prefix is null)
        {
            return null;
        }

        return new SemanticPrefixedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Prefix.Value);
    }

    private IPrefixedUnitInstanceSyntax CreateSyntax(PrefixedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new PrefixedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.PrefixLocation);
    }

    private sealed class PrefixedUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<MetricPrefixName, BinaryPrefixName>? Prefix { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location PrefixLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("MetricPrefix", Adapters.For<MetricPrefixName>(RecordMetricPrefix));
            yield return ("BinaryPrefix", Adapters.For<BinaryPrefixName>(RecordBinaryPrefix));
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

        private void RecordMetricPrefix(MetricPrefixName metricPrefix, Location location)
        {
            Prefix = metricPrefix;
            PrefixLocation = location;
        }

        private void RecordBinaryPrefix(BinaryPrefixName binaryPrefix, Location location)
        {
            Prefix = binaryPrefix;
            PrefixLocation = location;
        }
    }

    private sealed class SyntacticPrefixedUnitInstance : ASyntacticModifiedUnitInstance<IPrefixedUnitInstance, IPrefixedUnitInstanceSyntax>, ISyntacticPrefixedUnitInstance
    {
        public SyntacticPrefixedUnitInstance(IPrefixedUnitInstance semantics, IPrefixedUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        OneOf<MetricPrefixName, BinaryPrefixName> IPrefixedUnitInstance.Prefix => Semantics.Prefix;

        IPrefixedUnitInstanceSyntax ISyntacticPrefixedUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticPrefixedUnitInstance : ASemanticModifiedUnitInstance, IPrefixedUnitInstance
    {
        private OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }

        public SemanticPrefixedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<MetricPrefixName, BinaryPrefixName> prefix) : base(name, pluralForm, originalUnitInstance)
        {
            Prefix = prefix;
        }

        OneOf<MetricPrefixName, BinaryPrefixName> IPrefixedUnitInstance.Prefix => Prefix;
    }

    private sealed class PrefixedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IPrefixedUnitInstanceSyntax
    {
        private Location Prefix { get; }

        public PrefixedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm, Location originalUnitInstance, Location prefix) : base(attributeName, attribute, name, pluralForm, originalUnitInstance)
        {
            Prefix = prefix;
        }

        Location IPrefixedUnitInstanceSyntax.Prefix => Prefix;
    }
}
