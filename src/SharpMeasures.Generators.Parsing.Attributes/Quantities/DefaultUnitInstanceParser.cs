namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DefaultUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class DefaultUnitInstanceParser : ISyntacticDefaultUnitInstanceParser, ISemanticDefaultUnitInstanceParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DefaultUnitInstanceParser"/>, parsing the arguments of a <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DefaultUnitInstanceParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticDefaultUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DefaultUnitAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IDefaultUnitInstance? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        DefaultUnitAttributeArgumentRecorder recoder = new();

        if (SemanticParser.TryParse(recoder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recoder);
    }

    private static ISyntacticDefaultUnitInstance CreateSyntactic(DefaultUnitAttributeArgumentRecorder recorder)
    {
        return new SyntacticDefaultUnitInstance(CreateSemantic(recorder), CreateSyntax(recorder));
    }

    private static IDefaultUnitInstance CreateSemantic(DefaultUnitAttributeArgumentRecorder recorder)
    {
        return new SemanticDefaultUnitInstance(recorder.UnitInstance, recorder.Symbol);
    }

    private static IDefaultUnitInstanceSyntax CreateSyntax(DefaultUnitAttributeArgumentRecorder recorder)
    {
        return new DefaultUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.UnitInstanceLocation, recorder.SymbolLocation);
    }

    private sealed class DefaultUnitAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public string? UnitInstance { get; private set; }
        public string? Symbol { get; private set; }

        public Location UnitInstanceLocation { get; private set; } = Location.None;
        public Location SymbolLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("UnitInstance", Adapters.ForNullable<string>(RecordUnitInstance));
            yield return ("Symbol", Adapters.ForNullable<string>(RecordSymbol));
        }

        private void RecordUnitInstance(string? unitInstance, Location location)
        {
            UnitInstance = unitInstance;
            UnitInstanceLocation = location;
        }

        private void RecordSymbol(string? symbol, Location location)
        {
            Symbol = symbol;
            SymbolLocation = location;
        }
    }

    private sealed class SyntacticDefaultUnitInstance : ISyntacticDefaultUnitInstance
    {
        private IDefaultUnitInstance Semantics { get; }
        private IDefaultUnitInstanceSyntax Syntax { get; }

        public SyntacticDefaultUnitInstance(IDefaultUnitInstance semantics, IDefaultUnitInstanceSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        string? IDefaultUnitInstance.UnitInstance => Semantics.UnitInstance;
        string? IDefaultUnitInstance.Symbol => Semantics.Symbol;

        IDefaultUnitInstanceSyntax ISyntacticDefaultUnitInstance.Syntax => Syntax;
    }

    private sealed class SemanticDefaultUnitInstance : IDefaultUnitInstance
    {
        private string? UnitInstance { get; }
        private string? Symbol { get; }

        public SemanticDefaultUnitInstance(string? unitInstance, string? symbol)
        {
            UnitInstance = unitInstance;
            Symbol = symbol;
        }

        string? IDefaultUnitInstance.UnitInstance => UnitInstance;
        string? IDefaultUnitInstance.Symbol => Symbol;
    }

    private sealed class DefaultUnitInstanceSyntax : AAttributeSyntax, IDefaultUnitInstanceSyntax
    {
        private Location UnitInstance { get; }
        private Location Symbol { get; }

        public DefaultUnitInstanceSyntax(Location attributeName, Location attribute, Location unitInstance, Location symbol) : base(attributeName, attribute)
        {
            UnitInstance = unitInstance;
            Symbol = symbol;
        }

        Location IDefaultUnitInstanceSyntax.UnitInstance => UnitInstance;
        Location IDefaultUnitInstanceSyntax.Symbol => Symbol;
    }
}
