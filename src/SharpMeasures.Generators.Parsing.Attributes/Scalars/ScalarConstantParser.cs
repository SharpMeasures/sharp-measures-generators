namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarConstantAttribute"/> to be parsed.</summary>
public sealed class ScalarConstantParser : ISyntacticScalarConstantParser, ISemanticScalarConstantParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarConstantParser"/>, parsing the arguments of a <see cref="ScalarConstantAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarConstantParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticScalarConstant? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ScalarConstantAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IScalarConstant? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ScalarConstantAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticScalarConstant? CreateSyntactic(ScalarConstantAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IScalarConstant semantics)
        {
            return null;
        }

        return new SyntacticScalarConstant(semantics, CreateSyntax(recorder));
    }

    private static IScalarConstant? CreateSemantic(ScalarConstantAttributeArgumentRecorder recorder)
    {
        if (recorder.Value is null)
        {
            return null;
        }

        return new SemanticScalarConstant(recorder.Name, recorder.UnitInstance, recorder.Value.Value);
    }

    private static IScalarConstantSyntax CreateSyntax(ScalarConstantAttributeArgumentRecorder recorder)
    {
        return new ScalarConstantSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.UnitInstanceLocation, recorder.ValueLocation);
    }

    private sealed class ScalarConstantAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? UnitInstance { get; private set; }
        public OneOf<double, string?>? Value { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location UnitInstanceLocation { get; private set; } = Location.None;
        public Location ValueLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("UnitInstance", Adapters.ForNullable<string>(RecordUnitInstance));
            yield return ("Value", Adapters.For<double>(RecordValue));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordUnitInstance(string? unitInstance, Location location)
        {
            UnitInstance = unitInstance;
            UnitInstanceLocation = location;
        }

        private void RecordValue(double value, Location location)
        {
            Value = value;
            ValueLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Value = expression;
            ValueLocation = location;
        }
    }

    private sealed class SyntacticScalarConstant : ISyntacticScalarConstant
    {
        private IScalarConstant Semantics { get; }
        private IScalarConstantSyntax Syntax { get; }

        public SyntacticScalarConstant(IScalarConstant semantics, IScalarConstantSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        string? IScalarConstant.Name => Semantics.Name;
        string? IScalarConstant.UnitInstance => Semantics.UnitInstance;
        OneOf<double, string?> IScalarConstant.Value => Semantics.Value;

        IScalarConstantSyntax ISyntacticScalarConstant.Syntax => Syntax;
    }

    private sealed class SemanticScalarConstant : IScalarConstant
    {
        private string? Name { get; }
        private string? UnitInstance { get; }
        private OneOf<double, string?> Value { get; }

        public SemanticScalarConstant(string? name, string? unitInstance, OneOf<double, string?> value)
        {
            Name = name;
            UnitInstance = unitInstance;
            Value = value;
        }

        string? IScalarConstant.Name => Name;
        string? IScalarConstant.UnitInstance => UnitInstance;
        OneOf<double, string?> IScalarConstant.Value => Value;
    }

    private sealed class ScalarConstantSyntax : AAttributeSyntax, IScalarConstantSyntax
    {
        private Location Name { get; }
        private Location UnitInstance { get; }
        private Location Value { get; }

        public ScalarConstantSyntax(Location attributeName, Location attribute, Location name, Location unitInstance, Location value)
            : base(attributeName, attribute)
        {
            Name = name;
            UnitInstance = unitInstance;
            Value = value;
        }

        Location IScalarConstantSyntax.Name => Name;
        Location IScalarConstantSyntax.UnitInstance => UnitInstance;
        Location IScalarConstantSyntax.Value => Value;
    }
}
