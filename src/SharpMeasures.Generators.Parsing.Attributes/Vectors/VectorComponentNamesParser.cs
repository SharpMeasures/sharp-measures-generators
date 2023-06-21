namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorComponentNamesAttribute"/> to be parsed.</summary>
public sealed class VectorComponentNamesParser : ISyntacticVectorComponentNamesParser, ISemanticVectorComponentNamesParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorComponentNamesParser"/>, parsing the arguments of a <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorComponentNamesParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticVectorComponentNames? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorComponentNamesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IVectorComponentNames? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        VectorComponentNamesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticVectorComponentNames CreateSyntactic(VectorComponentNamesAttributeArgumentRecorder recorder)
    {
        return new SyntacticVectorComponentNames(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IVectorComponentNames CreateSemantic(VectorComponentNamesAttributeArgumentRecorder recorder)
    {
        return new SemanticVectorComponentNames(recorder.Names, recorder.Expression);
    }

    private static IVectorComponentNamesSyntax CreateSyntax(VectorComponentNamesAttributeArgumentRecorder recorder)
    {
        return new VectorComponentNamesSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.NamesCollectionLocation, recorder.NamesElementLocations, recorder.ExpressionLocation);
    }

    private sealed class VectorComponentNamesAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public IReadOnlyList<string?>? Names { get; private set; }
        public string? Expression { get; private set; }

        public Location NamesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> NamesElementLocations { get; private set; } = Array.Empty<Location>();
        public Location ExpressionLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Names", Adapters.ForNullable<string>(RecordNames));
        }

        private void RecordNames(IReadOnlyList<string?>? names, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Names = names;

            NamesCollectionLocation = collectionLocation;
            NamesElementLocations = elementLocations;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Expression = expression;
            ExpressionLocation = location;
        }
    }

    private sealed class SyntacticVectorComponentNames : ISyntacticVectorComponentNames
    {
        private IVectorComponentNames Semantics { get; }
        private IVectorComponentNamesSyntax Syntax { get; }

        public SyntacticVectorComponentNames(IVectorComponentNames semantics, IVectorComponentNamesSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        IReadOnlyList<string?>? IVectorComponentNames.Names => Semantics.Names;
        string? IVectorComponentNames.Expression => Semantics.Expression;

        IVectorComponentNamesSyntax ISyntacticVectorComponentNames.Syntax => Syntax;
    }

    private sealed class SemanticVectorComponentNames : IVectorComponentNames
    {
        private IReadOnlyList<string?>? Names { get; }
        private string? Expression { get; }

        public SemanticVectorComponentNames(IReadOnlyList<string?>? names, string? expression)
        {
            Names = names;
            Expression = expression;
        }

        IReadOnlyList<string?>? IVectorComponentNames.Names => Names;
        string? IVectorComponentNames.Expression => Expression;
    }

    private sealed class VectorComponentNamesSyntax : AAttributeSyntax, IVectorComponentNamesSyntax
    {
        private Location NamesCollection { get; }
        private IReadOnlyList<Location> NamesElements { get; }
        private Location Expression { get; }

        public VectorComponentNamesSyntax(Location attributeName, Location attribute, Location namesCollection, IReadOnlyList<Location> namesElements, Location expression) : base(attributeName, attribute)
        {
            NamesCollection = namesCollection;
            NamesElements = namesElements;
            Expression = expression;
        }

        Location IVectorComponentNamesSyntax.NamesCollection => NamesCollection;
        IReadOnlyList<Location> IVectorComponentNamesSyntax.NamesElements => NamesElements;
        Location IVectorComponentNamesSyntax.Expression => Expression;
    }
}
