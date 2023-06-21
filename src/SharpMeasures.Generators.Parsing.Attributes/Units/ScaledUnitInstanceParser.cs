namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScaledUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class ScaledUnitInstanceParser : ISyntacticScaledUnitInstanceParser, ISemanticScaledUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScaledUnitInstanceParser"/>, parsing the arguments of a <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScaledUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticScaledUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ScaledUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IScaledUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ScaledUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticScaledUnitInstance? CreateSyntactic(ScaledUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IScaledUnitInstance semantics)
        {
            return null;
        }

        return new SyntacticScaledUnitInstance(semantics, CreateSyntax(recorder));
    }

    private IScaledUnitInstance? CreateSemantic(ScaledUnitInstanceAttributeArgumentRecorder recorder)
    {
        if (recorder.Scale is null)
        {
            return null;
        }

        return new SemanticScaledUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Scale.Value);
    }

    private IScaledUnitInstanceSyntax CreateSyntax(ScaledUnitInstanceAttributeArgumentRecorder recorder)
    {
        return new ScaledUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.ScaleLocation);
    }

    private sealed class ScaledUnitInstanceAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<double, string?>? Scale { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location ScaleLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("Scale", Adapters.For<double>(RecordScale));
            yield return ("ScaleExpression", Adapters.ForNullable<string>(RecordScaleExpression));
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

        private void RecordScale(double scale, Location location)
        {
            Scale = scale;
            ScaleLocation = location;
        }

        private void RecordScaleExpression(string? expression, Location location)
        {
            Scale = expression;
            ScaleLocation = location;
        }
    }

    private sealed class SyntacticScaledUnitInstance : ASyntacticModifiedUnitInstance<IScaledUnitInstance, IScaledUnitInstanceSyntax>, ISyntacticScaledUnitInstance
    {
        public SyntacticScaledUnitInstance(IScaledUnitInstance semantics, IScaledUnitInstanceSyntax syntax) : base(semantics, syntax) { }

        OneOf<double, string?> IScaledUnitInstance.Scale => Semantics.Scale;

        IScaledUnitInstanceSyntax ISyntacticScaledUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticScaledUnitInstance : ASemanticModifiedUnitInstance, IScaledUnitInstance
    {
        private OneOf<double, string?> Scale { get; }

        public SemanticScaledUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<double, string?> scale) : base(name, pluralForm, originalUnitInstance)
        {
            Scale = scale;
        }

        OneOf<double, string?> IScaledUnitInstance.Scale => Scale;
    }

    internal sealed class ScaledUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IScaledUnitInstanceSyntax
    {
        private Location Scale { get; }

        public ScaledUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm, Location originalUnitInstance, Location scale) : base(attributeName, attribute, name, pluralForm, originalUnitInstance)
        {
            Scale = scale;
        }

        Location IScaledUnitInstanceSyntax.Scale => Scale;
    }
}
