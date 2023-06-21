namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityPropertyAttribute{TResult}"/> to be parsed.</summary>
public sealed class QuantityPropertyParser : ISyntacticQuantityPropertyParser, ISemanticQuantityPropertyParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityPropertyParser"/>, parsing the arguments of a <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityPropertyParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticQuantityProperty? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantityPropertyAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IQuantityProperty? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        QuantityPropertyAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticQuantityProperty? CreateSyntactic(QuantityPropertyAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IQuantityProperty semantics)
        {
            return null;
        }

        return new SyntacticQuantityProperty(semantics, CreateSyntax(recorder));
    }

    private static IQuantityProperty? CreateSemantic(QuantityPropertyAttributeArgumentRecorder recorder)
    {
        if (recorder.Result is null)
        {
            return null;
        }

        return new SemanticQuantityProperty(recorder.Result, recorder.Name, recorder.Expression);
    }

    private static IQuantityPropertySyntax CreateSyntax(QuantityPropertyAttributeArgumentRecorder recorder)
    {
        return new QuantityPropertySyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.ResultLocation, recorder.NameLocation, recorder.ExpressionLocation);
    }

    private sealed class QuantityPropertyAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Result { get; private set; }

        public string? Name { get; private set; }
        public string? Expression { get; private set; }

        public Location ResultLocation { get; private set; } = Location.None;

        public Location NameLocation { get; private set; } = Location.None;
        public Location ExpressionLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TResult", Adapters.For(RecordResult));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
        }

        private void RecordResult(ITypeSymbol result, Location location)
        {
            Result = result;
            ResultLocation = location;
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Expression = expression;
            ExpressionLocation = location;
        }
    }

    private sealed class SyntacticQuantityProperty : ISyntacticQuantityProperty
    {
        private IQuantityProperty Semantics { get; }
        private IQuantityPropertySyntax Syntax { get; }

        public SyntacticQuantityProperty(IQuantityProperty semantics, IQuantityPropertySyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IQuantityProperty.Result => Semantics.Result;

        string? IQuantityProperty.Name => Semantics.Name;
        string? IQuantityProperty.Expression => Semantics.Expression;

        IQuantityPropertySyntax ISyntacticQuantityProperty.Syntax => Syntax;
    }

    private sealed class SemanticQuantityProperty : IQuantityProperty
    {
        private ITypeSymbol Result { get; }
        private string? Name { get; }
        private string? Expression { get; }

        public SemanticQuantityProperty(ITypeSymbol result, string? name, string? expression)
        {
            Result = result;
            Name = name;
            Expression = expression;
        }

        ITypeSymbol IQuantityProperty.Result => Result;
        string? IQuantityProperty.Name => Name;
        string? IQuantityProperty.Expression => Expression;
    }

    private sealed class QuantityPropertySyntax : AAttributeSyntax, IQuantityPropertySyntax
    {
        private Location Result { get; }
        private Location Name { get; }
        private Location Expression { get; }

        public QuantityPropertySyntax(Location attributeName, Location attribute, Location result, Location name, Location expression) : base(attributeName, attribute)
        {
            Result = result;
            Name = name;
            Expression = expression;
        }

        Location IQuantityPropertySyntax.Result => Result;
        Location IQuantityPropertySyntax.Name => Name;
        Location IQuantityPropertySyntax.Expression => Expression;
    }
}
