namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="BiasedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class BiasedUnitInstanceParser : ISyntacticBiasedUnitInstanceParser, ISemanticBiasedUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="BiasedUnitInstanceParser"/>, parsing the arguments of a <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public BiasedUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticBiasedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        BiasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IBiasedUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        BiasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticBiasedUnitInstance? CreateSyntactic(BiasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IBiasedUnitInstance semantics)
        {
            return null;
        }

        return new SyntacticBiasedUnitInstance(semantics, CreateSyntax(recorder));
    }

    private IBiasedUnitInstance? CreateSemantic(BiasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (recorder.Bias is null)
        {
            return null;
        }

        return new SemanticBiasedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Bias.Value);
    }

    private IBiasedUnitInstanceSyntax CreateSyntax(BiasedUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new BiasedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.BiasLocation);
    }

    private sealed class BiasedUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<double, string?>? Bias { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location BiasLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("Bias", Adapters.For<double>(RecordBias));
            yield return ("BiasExpression", Adapters.ForNullable<string>(RecordBiasExpression));
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

        private void RecordBias(double bias, Location location)
        {
            Bias = bias;
            BiasLocation = location;
        }

        private void RecordBiasExpression(string? expression, Location location)
        {
            Bias = expression;
            BiasLocation = location;
        }
    }

    private sealed class SyntacticBiasedUnitInstance : ASyntacticModifiedUnitInstance<IBiasedUnitInstance, IBiasedUnitInstanceSyntax>, ISyntacticBiasedUnitInstance
    {
        public SyntacticBiasedUnitInstance(IBiasedUnitInstance semantics, IBiasedUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        OneOf<double, string?> IBiasedUnitInstance.Bias => Semantics.Bias;

        IBiasedUnitInstanceSyntax ISyntacticBiasedUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticBiasedUnitInstance : ASemanticModifiedUnitInstance, IBiasedUnitInstance
    {
        private OneOf<double, string?> Bias { get; }

        public SemanticBiasedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<double, string?> bias) : base(name, pluralForm, originalUnitInstance)
        {
            Bias = bias;
        }

        OneOf<double, string?> IBiasedUnitInstance.Bias => Bias;
    }

    private sealed class BiasedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IBiasedUnitInstanceSyntax
    {
        private Location Bias { get; }

        public BiasedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm, Location originalUnitInstance, Location bias) : base(attributeName, attribute, name, pluralForm, originalUnitInstance)
        {
            Bias = bias;
        }

        Location IBiasedUnitInstanceSyntax.Bias => Bias;
    }
}
