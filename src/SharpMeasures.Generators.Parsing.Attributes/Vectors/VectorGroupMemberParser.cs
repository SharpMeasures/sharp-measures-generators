namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorGroupMemberAttribute{TGroup}"/> to be parsed.</summary>
public sealed class VectorGroupMemberParser : ISyntacticVectorGroupMemberParser, ISemanticVectorGroupMemberParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorGroupMemberParser"/>, parsing the arguments of a <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorGroupMemberParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorGroupMember? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorGroupMemberAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorGroupMember? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorGroupMemberAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorGroupMember? CreateSyntactic(VectorGroupMemberAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IVectorGroupMember semantics)
        {
            return null;
        }

        return new SyntacticVectorGroupMember(semantics, CreateSyntax(recorder));
    }

    private static IVectorGroupMember? CreateSemantic(VectorGroupMemberAttributeArgumentRecorder recorder)
    {
        if (recorder.Group is null)
        {
            return null;
        }

        return new SemanticVectorGroupMember(recorder.Group, recorder.Dimension);
    }

    private static IVectorGroupMemberSyntax CreateSyntax(VectorGroupMemberAttributeArgumentRecorder recorder)
    {
        return new VectorGroupMemberSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.GroupLocation, recorder.DimensionLocation);
    }

    private sealed class VectorGroupMemberAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Group { get; private set; }
        public int? Dimension { get; private set; }

        public Location GroupLocation { get; private set; } = Location.None;
        public Location DimensionLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TGroup", Adapters.For(RecordGroup));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Dimension", Adapters.For<int>(RecordDimension));
        }

        private void RecordGroup(ITypeSymbol group, Location location)
        {
            Group = group;
            GroupLocation = location;
        }

        private void RecordDimension(int dimension, Location location)
        {
            Dimension = dimension;
            DimensionLocation = location;
        }
    }

    private sealed class SyntacticVectorGroupMember : ISyntacticVectorGroupMember
    {
        private IVectorGroupMember Semantics { get; }
        private IVectorGroupMemberSyntax Syntax { get; }

        public SyntacticVectorGroupMember(IVectorGroupMember semantics, IVectorGroupMemberSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IVectorGroupMember.Group => Semantics.Group;

        int? IVectorGroupMember.Dimension => Semantics.Dimension;

        IVectorGroupMemberSyntax ISyntacticVectorGroupMember.Syntax => Syntax;
    }

    private sealed class SemanticVectorGroupMember : IVectorGroupMember
    {
        private ITypeSymbol Group { get; }

        private int? Dimension { get; }

        public SemanticVectorGroupMember(ITypeSymbol group, int? dimension)
        {
            Group = group;

            Dimension = dimension;
        }

        ITypeSymbol IVectorGroupMember.Group => Group;

        int? IVectorGroupMember.Dimension => Dimension;
    }

    private sealed class VectorGroupMemberSyntax : AAttributeSyntax, IVectorGroupMemberSyntax
    {
        private Location Group { get; }
        private Location Dimension { get; }

        public VectorGroupMemberSyntax(Location attributeName, Location attribute, Location group, Location dimension) : base(attributeName, attribute)
        {
            Group = group;

            Dimension = dimension;
        }

        Location IVectorGroupMemberSyntax.Group => Group;
        Location IVectorGroupMemberSyntax.Dimension => Dimension;
    }
}
