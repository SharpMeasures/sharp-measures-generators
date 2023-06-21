namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarAssociationAttribute{TScalar}"/> to be parsed.</summary>
public sealed class ScalarAssociationParser : ISyntacticScalarAssociationParser, ISemanticScalarAssociationParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarAssociationParser"/>, parsing the arguments of a <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarAssociationParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticScalarAssociation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ScalarAssociationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IScalarAssociation? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        ScalarAssociationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticScalarAssociation? CreateSyntactic(ScalarAssociationAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IScalarAssociation semantics)
        {
            return null;
        }

        return new SyntacticScalarAssociation(semantics, CreateSyntax(recorder));
    }

    private static IScalarAssociation? CreateSemantic(ScalarAssociationAttributeArgumentRecorder recorder)
    {
        if (recorder.ScalarQuantity is null)
        {
            return null;
        }

        return new SemanticScalarAssociation(recorder.ScalarQuantity, recorder.AsComponents, recorder.AsMagnitude);
    }

    private static IScalarAssociationSyntax CreateSyntax(ScalarAssociationAttributeArgumentRecorder recorder)
    {
        return new ScalarAssociationSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.ScalarQuantityLocation, recorder.AsComponentsLocation, recorder.AsMagnitudeLocation);
    }

    private sealed class ScalarAssociationAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? ScalarQuantity { get; private set; }

        public bool? AsComponents { get; private set; }
        public bool? AsMagnitude { get; private set; }

        public Location ScalarQuantityLocation { get; private set; } = Location.None;

        public Location AsComponentsLocation { get; private set; } = Location.None;
        public Location AsMagnitudeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TScalar", Adapters.For(RecordScalarQuantity));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("AsComponents", Adapters.For<bool>(RecordAsComponents));
            yield return ("AsMagnitude", Adapters.For<bool>(RecordAsMagnitude));
        }

        private void RecordScalarQuantity(ITypeSymbol scalarQuantity, Location location)
        {
            ScalarQuantity = scalarQuantity;
            ScalarQuantityLocation = location;
        }

        private void RecordAsComponents(bool asComponents, Location location)
        {
            AsComponents = asComponents;
            AsComponentsLocation = location;
        }

        private void RecordAsMagnitude(bool asMagnitude, Location location)
        {
            AsMagnitude = asMagnitude;
            AsMagnitudeLocation = location;
        }
    }

    private sealed class SyntacticScalarAssociation : ISyntacticScalarAssociation
    {
        private IScalarAssociation Semantics { get; }
        private IScalarAssociationSyntax Syntax { get; }

        public SyntacticScalarAssociation(IScalarAssociation semantics, IScalarAssociationSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IScalarAssociation.ScalarQuantity => Semantics.ScalarQuantity;

        bool? IScalarAssociation.AsComponents => Semantics.AsComponents;
        bool? IScalarAssociation.AsMagnitude => Semantics.AsMagnitude;

        IScalarAssociationSyntax ISyntacticScalarAssociation.Syntax => Syntax;
    }

    private sealed class SemanticScalarAssociation : IScalarAssociation
    {
        private ITypeSymbol ScalarQuantity { get; }

        private bool? AsComponents { get; }
        private bool? AsMagnitude { get; }

        public SemanticScalarAssociation(ITypeSymbol scalarQuantity, bool? asComponents, bool? asMagnitude)
        {
            ScalarQuantity = scalarQuantity;

            AsComponents = asComponents;
            AsMagnitude = asMagnitude;
        }

        ITypeSymbol IScalarAssociation.ScalarQuantity => ScalarQuantity;

        bool? IScalarAssociation.AsComponents => AsComponents;
        bool? IScalarAssociation.AsMagnitude => AsMagnitude;
    }

    private sealed class ScalarAssociationSyntax : AAttributeSyntax, IScalarAssociationSyntax
    {
        private Location ScalarQuantity { get; }

        private Location AsComponents { get; }
        private Location AsMagnitude { get; }

        public ScalarAssociationSyntax(Location attributeName, Location attribute, Location scalarQuantity, Location asComponents, Location asMagnitude) : base(attributeName, attribute)
        {
            ScalarQuantity = scalarQuantity;

            AsComponents = asComponents;
            AsMagnitude = asMagnitude;
        }

        Location IScalarAssociationSyntax.ScalarQuantity => ScalarQuantity;

        Location IScalarAssociationSyntax.AsComponents => AsComponents;
        Location IScalarAssociationSyntax.AsMagnitude => AsMagnitude;
    }
}
