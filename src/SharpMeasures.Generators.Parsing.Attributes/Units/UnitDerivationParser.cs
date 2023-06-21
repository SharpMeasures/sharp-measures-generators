namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitDerivationAttribute"/> to be parsed.</summary>
public sealed class UnitDerivationParser : ISyntacticUnitDerivationParser, ISemanticUnitDerivationParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitDerivationParser"/>, parsing the arguments of a <see cref="UnitDerivationAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitDerivationParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticUnitDerivation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        UnitDerivationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IUnitDerivation? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        UnitDerivationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private ISyntacticUnitDerivation CreateSyntactic(UnitDerivationAttributeArgumentRecorder recorder)
    {
        return new SyntacticUnitDerivation(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private IUnitDerivation CreateSemantic(UnitDerivationAttributeArgumentRecorder recorder)
    {
        return new SemanticUnitDerivation(recorder.DerivationID, recorder.Expression, recorder.Signature, recorder.MethodName);
    }

    private IUnitDerivationSyntax CreateSyntax(UnitDerivationAttributeArgumentRecorder recorder)
    {
        return new UnitDerivationSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.DerivationIDLocation, recorder.ExpressionLocation, recorder.SignatureCollectionLocation, recorder.SignatureElementLocations, recorder.MethodNameLocation);
    }

    private sealed class UnitDerivationAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? DerivationID { get; private set; }
        public string? Expression { get; private set; }
        public IReadOnlyList<ITypeSymbol?>? Signature { get; private set; }
        public string? MethodName { get; private set; }

        public Location DerivationIDLocation { get; private set; } = Location.None;
        public Location ExpressionLocation { get; private set; } = Location.None;
        public Location SignatureCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> SignatureElementLocations { get; private set; } = Array.Empty<Location>();
        public Location MethodNameLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("DerivationID", Adapters.ForNullable<string>(RecordDerivationID));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
            yield return ("MethodName", Adapters.ForNullable<string>(RecordMethodName));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Signature", Adapters.ForNullable<ITypeSymbol>(RecordSignature));
        }

        private void RecordDerivationID(string? derivationID, Location location)
        {
            if (derivationID is not null)
            {
                DerivationID = derivationID;
            }

            DerivationIDLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Expression = expression;
            ExpressionLocation = location;
        }

        private void RecordSignature(IReadOnlyList<ITypeSymbol?>? signature, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Signature = signature;
            SignatureCollectionLocation = collectionLocation;
            SignatureElementLocations = elementLocations;
        }

        private void RecordMethodName(string? methodName, Location location)
        {
            MethodName = methodName;
            MethodNameLocation = location;
        }
    }

    private sealed class SyntacticUnitDerivation : ISyntacticUnitDerivation
    {
        private IUnitDerivation Semantics { get; }
        private IUnitDerivationSyntax Syntax { get; }

        public SyntacticUnitDerivation(IUnitDerivation semantics, IUnitDerivationSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        string? IUnitDerivation.DerivationID => Semantics.DerivationID;
        string? IUnitDerivation.Expression => Semantics.Expression;
        IReadOnlyList<ITypeSymbol?>? IUnitDerivation.Signature => Semantics.Signature;
        string? IUnitDerivation.MethodName => Semantics.MethodName;

        IUnitDerivationSyntax ISyntacticUnitDerivation.Syntax => Syntax;
    }

    private sealed class SemanticUnitDerivation : IUnitDerivation
    {
        private string? DerivationID { get; }
        private string? Expression { get; }
        private IReadOnlyList<ITypeSymbol?>? Signature { get; }
        private string? MethodName { get; }

        public SemanticUnitDerivation(string? derivationID, string? expression, IReadOnlyList<ITypeSymbol?>? signature, string? methodName)
        {
            DerivationID = derivationID;
            Expression = expression;
            Signature = signature;
            MethodName = methodName;
        }

        string? IUnitDerivation.DerivationID => DerivationID;
        string? IUnitDerivation.Expression => Expression;
        IReadOnlyList<ITypeSymbol?>? IUnitDerivation.Signature => Signature;
        string? IUnitDerivation.MethodName => MethodName;
    }

    private sealed class UnitDerivationSyntax : AAttributeSyntax, IUnitDerivationSyntax
    {
        private Location DerivationID { get; }
        private Location Expression { get; }
        private Location SignatureCollection { get; }
        private IReadOnlyList<Location> SignatureElements { get; }
        private Location MethodName { get; }

        public UnitDerivationSyntax(Location attributeName, Location attribute, Location derivationID, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements, Location methodName)
            : base(attributeName, attribute)
        {
            DerivationID = derivationID;
            Expression = expression;
            SignatureCollection = signatureCollection;
            SignatureElements = signatureElements;
            MethodName = methodName;
        }

        Location IUnitDerivationSyntax.DerivationID => DerivationID;
        Location IUnitDerivationSyntax.Expression => Expression;

        Location IUnitDerivationSyntax.SignatureCollection => SignatureCollection;
        IReadOnlyList<Location> IUnitDerivationSyntax.SignatureElements => SignatureElements;

        Location IUnitDerivationSyntax.MethodName => MethodName;
    }
}
