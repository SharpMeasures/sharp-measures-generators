namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorAssociationAttribute{TVector}"/> to be parsed.</summary>
public sealed class VectorAssociationParser : ISyntacticVectorAssociationParser, ISemanticVectorAssociationParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorAssociationParser"/>, parsing the arguments of a <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorAssociationParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorAssociation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorAssociationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorAssociation? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorAssociationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorAssociation? CreateSyntactic(VectorAssociationAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IVectorAssociation semantics)
        {
            return null;
        }

        return new SyntacticVectorAssociation(semantics, CreateSyntax(recorder));
    }

    private static IVectorAssociation? CreateSemantic(VectorAssociationAttributeArgumentRecorder recorder)
    {
        if (recorder.VectorQuantity is null)
        {
            return null;
        }

        return new SemanticVectorAssociation(recorder.VectorQuantity);
    }

    private static IVectorAssociationSyntax CreateSyntax(VectorAssociationAttributeArgumentRecorder recorder)
    {
        return new VectorAssociationSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.VectorQuantityLocation);
    }

    private sealed class VectorAssociationAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? VectorQuantity { get; private set; }

        public Location VectorQuantityLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TVector", Adapters.For(RecordVectorQuantity));
        }

        private void RecordVectorQuantity(ITypeSymbol vectorQuantity, Location location)
        {
            VectorQuantity = vectorQuantity;
            VectorQuantityLocation = location;
        }
    }

    private sealed class SyntacticVectorAssociation : ISyntacticVectorAssociation
    {
        private IVectorAssociation Semantics { get; }
        private IVectorAssociationSyntax Syntax { get; }

        public SyntacticVectorAssociation(IVectorAssociation semantics, IVectorAssociationSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IVectorAssociation.VectorQuantity => Semantics.VectorQuantity;

        IVectorAssociationSyntax ISyntacticVectorAssociation.Syntax => Syntax;
    }

    private sealed class SemanticVectorAssociation : IVectorAssociation
    {
        private ITypeSymbol VectorQuantity { get; }

        public SemanticVectorAssociation(ITypeSymbol vectorQuantity)
        {
            VectorQuantity = vectorQuantity;
        }

        ITypeSymbol IVectorAssociation.VectorQuantity => VectorQuantity;
    }

    private sealed class VectorAssociationSyntax : AAttributeSyntax, IVectorAssociationSyntax
    {
        private Location VectorQuantity { get; }

        public VectorAssociationSyntax(Location attributeName, Location attribute, Location vectorQuantity) : base(attributeName, attribute)
        {
            VectorQuantity = vectorQuantity;
        }

        Location IVectorAssociationSyntax.VectorQuantity => VectorQuantity;
    }
}
