namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorConstantAttribute"/> to be parsed.</summary>
public sealed class VectorConstantParser : ISyntacticVectorConstantParser, ISemanticVectorConstantParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorConstantParser"/>, parsing the arguments of a <see cref="VectorConstantAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorConstantParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorConstant? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorConstantAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorConstant? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorConstantAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorConstant CreateSyntactic(VectorConstantAttributeArgumentRecorder recorder)
    {
        return new SyntacticVectorConstant(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IVectorConstant CreateSemantic(VectorConstantAttributeArgumentRecorder recorder)
    {
        return new SemanticVectorConstant(recorder.Name, recorder.UnitInstanceName, recorder.Value);
    }

    private static IVectorConstantSyntax CreateSyntax(VectorConstantAttributeArgumentRecorder recorder)
    {
        return new VectorConstantSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NameLocation, recorder.UnitInstanceNameLocation, recorder.ValueCollectionLocation, recorder.ValueElementLocations);
    }

    private sealed class VectorConstantAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? UnitInstanceName { get; private set; }
        public OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location UnitInstanceNameLocation { get; private set; } = Location.None;
        public Location ValueCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> ValueElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("UnitInstanceName", Adapters.ForNullable<string>(RecordUnitInstanceName));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Value", Adapters.ForNullableCollection<double>(RecordValue));
            yield return ("Expressions", Adapters.ForNullable<string>(RecordExpressions));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordUnitInstanceName(string? unitInstanceName, Location location)
        {
            UnitInstanceName = unitInstanceName;
            UnitInstanceNameLocation = location;
        }

        private void RecordValue(IReadOnlyList<double>? value, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Value = OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT0(value);

            ValueCollectionLocation = collectionLocation;
            ValueElementLocations = elementLocations;
        }

        private void RecordExpressions(IReadOnlyList<string?>? expressions, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Value = OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT1(expressions);

            ValueCollectionLocation = collectionLocation;
            ValueElementLocations = elementLocations;
        }
    }

    private sealed class SyntacticVectorConstant : ISyntacticVectorConstant
    {
        private IVectorConstant Semantics { get; }
        private IVectorConstantSyntax Syntax { get; }

        public SyntacticVectorConstant(IVectorConstant semantics, IVectorConstantSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        string? IVectorConstant.Name => Semantics.Name;
        string? IVectorConstant.UnitInstanceName => Semantics.UnitInstanceName;
        OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> IVectorConstant.Value => Semantics.Value;

        IVectorConstantSyntax ISyntacticVectorConstant.Syntax => Syntax;
    }

    private sealed class SemanticVectorConstant : IVectorConstant
    {
        private string? Name { get; }
        private string? UnitInstanceName { get; }
        private OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }

        public SemanticVectorConstant(string? name, string? unitInstanceName, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> value)
        {
            Name = name;
            UnitInstanceName = unitInstanceName;
            Value = value;
        }

        string? IVectorConstant.Name => Name;
        string? IVectorConstant.UnitInstanceName => UnitInstanceName;
        OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> IVectorConstant.Value => Value;
    }

    private sealed class VectorConstantSyntax : AAttributeSyntax, IVectorConstantSyntax
    {
        private Location Name { get; }
        private Location UnitInstanceName { get; }
        private Location ValueCollection { get; }
        private IReadOnlyList<Location> ValueElements { get; }

        public VectorConstantSyntax(Location attributeName, Location attribute, Location name, Location unitInstanceName, Location valueCollection, IReadOnlyList<Location> valueElements) : base(attributeName, attribute)
        {
            Name = name;
            UnitInstanceName = unitInstanceName;
            ValueCollection = valueCollection;
            ValueElements = valueElements;
        }

        Location IVectorConstantSyntax.Name => Name;
        Location IVectorConstantSyntax.UnitInstanceName => UnitInstanceName;
        Location IVectorConstantSyntax.ValueCollection => ValueCollection;
        IReadOnlyList<Location> IVectorConstantSyntax.ValueElements => ValueElements;
    }
}
