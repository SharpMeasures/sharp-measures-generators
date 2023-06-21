namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityConversionAttribute"/> to be parsed.</summary>
public sealed class QuantityConversionParser : ISyntacticQuantityConversionParser, ISemanticQuantityConversionParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityConversionParser"/>, parsing the arguments of a <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityConversionParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticQuantityConversion? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantityConversionAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IQuantityConversion? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        QuantityConversionAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticQuantityConversion CreateSyntactic(QuantityConversionAttributeArgumentRecorder recorder)
    {
        return new SyntacticQuantityConversion(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IQuantityConversion CreateSemantic(QuantityConversionAttributeArgumentRecorder recorder)
    {
        return new SemanticQuantityConversion(recorder.Quantities, recorder.ForwardsImplementation, recorder.ForwarsBehaviour, recorder.ForwardsPropertyName, recorder.ForwardsMethodName,
            recorder.ForwardsStaticMethodName, recorder.BackwardsImplementation, recorder.BackwardsBehaviour, recorder.BackwardsStaticMethodName);
    }

    private static IQuantityConversionSyntax CreateSyntax(QuantityConversionAttributeArgumentRecorder recorder)
    {
        return new ConvertibleQuantitySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.QuantitiesCollectionLocation, recorder.QuantitiesElementLocations,
            recorder.ForwardsImplementationLocation, recorder.ForwarsBehaviourLocation, recorder.ForwardsPropertyNameLocation, recorder.ForwardsMethodNameLocation,
            recorder.ForwardsStaticMethodNameLocation, recorder.BackwardsImplementationLocation, recorder.BackwardsBehaviourLocation, recorder.BackwardsStaticMethodNameLocation);
    }

    private sealed class QuantityConversionAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public IReadOnlyList<ITypeSymbol?>? Quantities { get; private set; }
        public ConversionImplementation? ForwardsImplementation { get; private set; }
        public ConversionOperatorBehaviour? ForwarsBehaviour { get; private set; }
        public string? ForwardsPropertyName { get; private set; }
        public string? ForwardsMethodName { get; private set; }
        public string? ForwardsStaticMethodName { get; private set; }
        public ConversionImplementation? BackwardsImplementation { get; private set; }
        public ConversionOperatorBehaviour? BackwardsBehaviour { get; private set; }
        public string? BackwardsStaticMethodName { get; private set; }

        public Location QuantitiesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> QuantitiesElementLocations { get; private set; } = Array.Empty<Location>();
        public Location ForwardsImplementationLocation { get; private set; } = Location.None;
        public Location ForwarsBehaviourLocation { get; private set; } = Location.None;
        public Location ForwardsPropertyNameLocation { get; private set; } = Location.None;
        public Location ForwardsMethodNameLocation { get; private set; } = Location.None;
        public Location ForwardsStaticMethodNameLocation { get; private set; } = Location.None;
        public Location BackwardsImplementationLocation { get; private set; } = Location.None;
        public Location BackwardsBehaviourLocation { get; private set; } = Location.None;
        public Location BackwardsStaticMethodNameLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("ForwardsImplementation", Adapters.For<ConversionImplementation>(RecordForwardsImplementation));
            yield return ("ForwardsBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordForwardsBehaviour));
            yield return ("ForwardsPropertyName", Adapters.ForNullable<string>(RecordForwardsPropertyName));
            yield return ("ForwardsMethodName", Adapters.ForNullable<string>(RecordForwardsMethodName));
            yield return ("ForwardsStaticMethodName", Adapters.ForNullable<string>(RecordForwardsStaticMethodName));
            yield return ("BackwardsImplementation", Adapters.For<ConversionImplementation>(RecordBackwardsImplementation));
            yield return ("BackwardsBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordBackwardsBehaviour));
            yield return ("BackwardsStaticMethodName", Adapters.ForNullable<string>(RecordBackwardsStaticMethodName));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Quantities", Adapters.ForNullable<ITypeSymbol>(RecordQuantities));
        }

        private void RecordQuantities(IReadOnlyList<ITypeSymbol?>? quantities, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Quantities = quantities;
            QuantitiesCollectionLocation = collectionLocation;
            QuantitiesElementLocations = elementLocations;
        }

        private void RecordForwardsImplementation(ConversionImplementation forwardsImplementation, Location location)
        {
            ForwardsImplementation = forwardsImplementation;
            ForwardsImplementationLocation = location;
        }

        private void RecordForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour, Location location)
        {
            ForwarsBehaviour = forwardsBehaviour;
            ForwarsBehaviourLocation = location;
        }

        private void RecordForwardsPropertyName(string? forwardsPropertyName, Location location)
        {
            ForwardsPropertyName = forwardsPropertyName;
            ForwardsPropertyNameLocation = location;
        }

        private void RecordForwardsMethodName(string? forwardsMethodName, Location location)
        {
            ForwardsMethodName = forwardsMethodName;
            ForwardsMethodNameLocation = location;
        }

        private void RecordForwardsStaticMethodName(string? forwardsStaticMethodName, Location location)
        {
            ForwardsStaticMethodName = forwardsStaticMethodName;
            ForwardsStaticMethodNameLocation = location;
        }

        private void RecordBackwardsImplementation(ConversionImplementation backwardsImplementation, Location location)
        {
            BackwardsImplementation = backwardsImplementation;
            BackwardsImplementationLocation = location;
        }

        private void RecordBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour, Location location)
        {
            BackwardsBehaviour = backwardsBehaviour;
            BackwardsBehaviourLocation = location;
        }

        private void RecordBackwardsStaticMethodName(string? backwardsStaticMethodName, Location location)
        {
            BackwardsStaticMethodName = backwardsStaticMethodName;
            BackwardsStaticMethodNameLocation = location;
        }
    }

    private sealed class SyntacticQuantityConversion : ISyntacticQuantityConversion
    {
        private IQuantityConversion Semantics { get; }
        private IQuantityConversionSyntax Syntax { get; }

        public SyntacticQuantityConversion(IQuantityConversion semantics, IQuantityConversionSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<ITypeSymbol?>? IQuantityConversion.Quantities => Semantics.Quantities;

        ConversionImplementation? IQuantityConversion.ForwardsImplementation => Semantics.ForwardsImplementation;
        ConversionOperatorBehaviour? IQuantityConversion.ForwardsBehaviour => Semantics.ForwardsBehaviour;
        string? IQuantityConversion.ForwardsPropertyName => Semantics.ForwardsPropertyName;
        string? IQuantityConversion.ForwardsMethodName => Semantics.ForwardsMethodName;
        string? IQuantityConversion.ForwardsStaticMethodName => Semantics.ForwardsStaticMethodName;
        ConversionOperatorBehaviour? IQuantityConversion.BackwardsBehaviour => Semantics.BackwardsBehaviour;
        ConversionImplementation? IQuantityConversion.BackwardsImplementation => Semantics.BackwardsImplementation;
        string? IQuantityConversion.BackwardsStaticMethodName => Semantics.BackwardsStaticMethodName;

        IQuantityConversionSyntax ISyntacticQuantityConversion.Syntax => Syntax;
    }

    private sealed class SemanticQuantityConversion : IQuantityConversion
    {
        private IReadOnlyList<ITypeSymbol?>? Quantities { get; }

        private ConversionImplementation? ForwardsImplementation { get; }
        private ConversionOperatorBehaviour? ForwardsBehaviour { get; }
        private string? ForwardsPropertyName { get; }
        private string? ForwardsMethodName { get; }
        private string? ForwardsStaticMethodName { get; }
        private ConversionImplementation? BackwardsImplementation { get; }
        private ConversionOperatorBehaviour? BackwardsBehaviour { get; }
        private string? BackwardsStaticMethodName { get; }

        public SemanticQuantityConversion(IReadOnlyList<ITypeSymbol?>? quantities, ConversionImplementation? forwardsImplementation, ConversionOperatorBehaviour? forwardsBehaviour, string? forwardsPropertyName,
            string? forwardsMethodName, string? forwardsStaticMethodName, ConversionImplementation? backwardsImplementation, ConversionOperatorBehaviour? backwardsBehaviour, string? backwardsStaticMethodName)
        {
            Quantities = quantities;

            ForwardsImplementation = forwardsImplementation;
            ForwardsBehaviour = forwardsBehaviour;
            ForwardsPropertyName = forwardsPropertyName;
            ForwardsMethodName = forwardsMethodName;
            ForwardsStaticMethodName = forwardsStaticMethodName;

            BackwardsImplementation = backwardsImplementation;
            BackwardsBehaviour = backwardsBehaviour;
            BackwardsStaticMethodName = backwardsStaticMethodName;
        }

        IReadOnlyList<ITypeSymbol?>? IQuantityConversion.Quantities => Quantities;

        ConversionImplementation? IQuantityConversion.ForwardsImplementation => ForwardsImplementation;
        ConversionOperatorBehaviour? IQuantityConversion.ForwardsBehaviour => ForwardsBehaviour;
        string? IQuantityConversion.ForwardsPropertyName => ForwardsPropertyName;
        string? IQuantityConversion.ForwardsMethodName => ForwardsMethodName;
        string? IQuantityConversion.ForwardsStaticMethodName => ForwardsStaticMethodName;
        ConversionOperatorBehaviour? IQuantityConversion.BackwardsBehaviour => BackwardsBehaviour;
        ConversionImplementation? IQuantityConversion.BackwardsImplementation => BackwardsImplementation;
        string? IQuantityConversion.BackwardsStaticMethodName => BackwardsStaticMethodName;
    }

    private sealed class ConvertibleQuantitySyntax : AAttributeSyntax, IQuantityConversionSyntax
    {
        private Location QuantitiesCollection { get; }
        private IReadOnlyList<Location> QuantitiesElements { get; }

        private Location ForwardsImplementation { get; }
        private Location ForwardsBehaviour { get; }
        private Location ForwardsPropertyName { get; }
        private Location ForwardsMethodName { get; }
        private Location ForwardsStaticMethodName { get; }
        private Location BackwardsImplementation { get; }
        private Location BackwardsBehaviour { get; }
        private Location BackwardsStaticMethodName { get; }

        public ConvertibleQuantitySyntax(Location attributeName, Location attribute, Location quantitiesCollection, IReadOnlyList<Location> quantitiesElements, Location forwardsImplementation, Location forwardsBehaviour, Location forwardsPropertyName,
            Location forwardsMethodName, Location forwardsStaticMethodName, Location backwardsImplementation, Location backwardsBehaviour, Location backwardsStaticMethodName)
            : base(attributeName, attribute)
        {
            QuantitiesCollection = quantitiesCollection;
            QuantitiesElements = quantitiesElements;

            ForwardsImplementation = forwardsImplementation;
            ForwardsBehaviour = forwardsBehaviour;
            ForwardsPropertyName = forwardsPropertyName;
            ForwardsMethodName = forwardsMethodName;
            ForwardsStaticMethodName = forwardsStaticMethodName;

            BackwardsImplementation = backwardsImplementation;
            BackwardsBehaviour = backwardsBehaviour;
            BackwardsStaticMethodName = backwardsStaticMethodName;
        }

        Location IQuantityConversionSyntax.QuantitiesCollection => QuantitiesCollection;
        IReadOnlyList<Location> IQuantityConversionSyntax.QuantitiesElements => QuantitiesElements;

        Location IQuantityConversionSyntax.ForwardsImplementation => ForwardsImplementation;
        Location IQuantityConversionSyntax.ForwardsBehaviour => ForwardsBehaviour;
        Location IQuantityConversionSyntax.ForwardsPropertyName => ForwardsPropertyName;
        Location IQuantityConversionSyntax.ForwardsMethodName => ForwardsMethodName;
        Location IQuantityConversionSyntax.ForwardsStaticMethodName => ForwardsStaticMethodName;
        Location IQuantityConversionSyntax.BackwardsBehaviour => BackwardsBehaviour;
        Location IQuantityConversionSyntax.BackwardsImplementation => BackwardsImplementation;
        Location IQuantityConversionSyntax.BackwardsStaticMethodName => BackwardsStaticMethodName;
    }
}
