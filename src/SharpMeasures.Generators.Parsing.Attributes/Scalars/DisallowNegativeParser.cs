namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DisallowNegativeAttribute"/> to be parsed.</summary>
public sealed class DisallowNegativeParser : ISyntacticDisallowNegativeParser, ISemanticDisallowNegativeParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DisallowNegativeParser"/>, parsing the arguments of a <see cref="DisallowNegativeAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DisallowNegativeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticDisallowNegative? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DisallowNegativeAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IDisallowNegative? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        DisallowNegativeAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticDisallowNegative CreateSyntactic(DisallowNegativeAttributeArgumentRecorder recorder)
    {
        return new SyntacticDisallowNegative(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IDisallowNegative CreateSemantic(DisallowNegativeAttributeArgumentRecorder recorder)
    {
        return new SemanticDisallowNegative(recorder.Behaviour);
    }

    private static IDisallowNegativeSyntax CreateSyntax(DisallowNegativeAttributeArgumentRecorder recorder)
    {
        return new DisallowNegativeSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.BehaviourLocation);
    }

    private sealed class DisallowNegativeAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public DisallowNegativeBehaviour? Behaviour { get; private set; }

        public Location BehaviourLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Behaviour", Adapters.For<DisallowNegativeBehaviour>(RecordUnitInstances));
        }

        private void RecordUnitInstances(DisallowNegativeBehaviour behaviour, Location location)
        {
            Behaviour = behaviour;
            BehaviourLocation = location;
        }
    }

    private sealed class SyntacticDisallowNegative : ISyntacticDisallowNegative
    {
        private IDisallowNegative Semantics { get; }
        private IDisallowNegativeSyntax Syntax { get; }

        public SyntacticDisallowNegative(IDisallowNegative semantics, IDisallowNegativeSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        DisallowNegativeBehaviour? IDisallowNegative.Behaviour => Semantics.Behaviour;

        IDisallowNegativeSyntax ISyntacticDisallowNegative.Syntax => Syntax;
    }

    private sealed class SemanticDisallowNegative : IDisallowNegative
    {
        private DisallowNegativeBehaviour? Behaviour { get; }

        public SemanticDisallowNegative(DisallowNegativeBehaviour? behaviour)
        {
            Behaviour = behaviour;
        }

        DisallowNegativeBehaviour? IDisallowNegative.Behaviour => Behaviour;
    }

    private sealed class DisallowNegativeSyntax : AAttributeSyntax, IDisallowNegativeSyntax
    {
        private Location Behaviour { get; }

        public DisallowNegativeSyntax(Location attributeName, Location attribute, Location behaviour)
            : base(attributeName, attribute)
        {
            Behaviour = behaviour;
        }

        Location IDisallowNegativeSyntax.Behaviour => Behaviour;
    }
}
