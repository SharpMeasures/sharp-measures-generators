namespace SharpMeasures.Generators.Types.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Documentation;
using SharpMeasures.Generators.Attributes.Parsing.Documentation;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Units;
using SharpMeasures.Generators.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <inheritdoc cref="IUnitTypeParser"/>
public sealed class UnitTypeParser : IUnitTypeParser
{
    private IUnitParser UnitParser { get; }
    private IUnitInstanceParser UnitInstanceParser { get; }

    private IDisableDocumentationRecordFactory DisableDocumentationRecordFactory { get; }
    private IEnableDocumentationRecordFactory EnableDocumentationRecordFactory { get; }

    /// <summary>Instantiates a <see cref="UnitTypeParser"/>, parsing <see cref="IUnitType"/>.</summary>
    /// <param name="unitParser">Produces records of <see cref="UnitAttribute{TScalar}"/>.</param>
    /// <param name="unitInstanceParser">Produces records of <see cref="UnitInstanceAttribute"/>.</param>
    /// <param name="disableDocumentationRecordFactory">Produces records of <see cref="DisableDocumentationAttribute"/>.</param>
    /// <param name="enableDocumentationRecordFactory">Produces records of <see cref="EnableDocumentationAttribute"/>.</param>
    public UnitTypeParser(IUnitParser unitParser, IUnitInstanceParser unitInstanceParser, IDisableDocumentationRecordFactory disableDocumentationRecordFactory, IEnableDocumentationRecordFactory enableDocumentationRecordFactory)
    {
        UnitParser = unitParser ?? throw new ArgumentNullException(nameof(unitParser));
        UnitInstanceParser = unitInstanceParser ?? throw new ArgumentNullException(nameof(unitInstanceParser));

        DisableDocumentationRecordFactory = disableDocumentationRecordFactory ?? throw new ArgumentNullException(nameof(disableDocumentationRecordFactory));
        EnableDocumentationRecordFactory = enableDocumentationRecordFactory ?? throw new ArgumentNullException(nameof(enableDocumentationRecordFactory));
    }

    async Task<IUnitType?> IUnitTypeParser.TryParse(ITypeSymbol type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        var attributes = type.GetAttributes();

        var unitDefinition = await ParseUnitDefinition(attributes);

        if (unitDefinition is null)
        {
            return null;
        }

        var disableDocumentation = await ParseDisableDocumentation(attributes);
        var enableDocumentation = await ParseEnableDocumentation(attributes);

        var unitInstances = await ParseUnitInstances(type);

        return new UnitType(type, unitDefinition, disableDocumentation, enableDocumentation, unitInstances);
    }

    private async Task<IUnitRecord?> ParseUnitDefinition(IReadOnlyList<AttributeData> attributes)
    {
        if (AttributeClassMatcher.GetFirst(typeof(UnitAttribute<>), attributes) is not AttributeData attribute)
        {
            return null;
        }

        if (attribute.ApplicationSyntaxReference is null || await attribute.ApplicationSyntaxReference.GetSyntaxAsync() is not AttributeSyntax attributeSyntax)
        {
            return null;
        }

        return UnitParser.TryParse(attribute, attributeSyntax);
    }

    private async Task<IDisableDocumentationRecord?> ParseDisableDocumentation(IReadOnlyList<AttributeData> attributes)
    {
        if (AttributeClassMatcher.GetFirst(typeof(DisableDocumentationAttribute), attributes) is not AttributeData attribute)
        {
            return null;
        }

        if (attribute.ApplicationSyntaxReference is null || await attribute.ApplicationSyntaxReference.GetSyntaxAsync() is not AttributeSyntax attributeSyntax)
        {
            return null;
        }

        return DisableDocumentationRecordFactory.Create(attributeSyntax);
    }

    private async Task<IEnableDocumentationRecord?> ParseEnableDocumentation(IReadOnlyList<AttributeData> attributes)
    {
        if (AttributeClassMatcher.GetFirst(typeof(EnableDocumentationAttribute), attributes) is not AttributeData attribute)
        {
            return null;
        }

        if (attribute.ApplicationSyntaxReference is null || await attribute.ApplicationSyntaxReference.GetSyntaxAsync() is not AttributeSyntax attributeSyntax)
        {
            return null;
        }

        return EnableDocumentationRecordFactory.Create(attributeSyntax);
    }

    private async Task<IReadOnlyDictionary<IPropertySymbol, IUnitInstanceRecord>> ParseUnitInstances(ITypeSymbol type)
    {
        Dictionary<IPropertySymbol, IUnitInstanceRecord> unitInstances = new(SymbolEqualityComparer.Default);

        var members = type.GetMembers();

        foreach (var member in members)
        {
            if (member is IPropertySymbol property && property.IsStatic && property.DeclaredAccessibility == Accessibility.Public && SymbolEqualityComparer.Default.Equals(property.GetMethod?.ReturnType, type))
            {
                if (AttributeClassMatcher.GetFirst(typeof(UnitInstanceAttribute), property.GetAttributes()) is not AttributeData attribute)
                {
                    continue;
                }

                if (attribute.ApplicationSyntaxReference is null || await attribute.ApplicationSyntaxReference.GetSyntaxAsync() is not AttributeSyntax attributeSyntax)
                {
                    continue;
                }

                if (UnitInstanceParser.TryParse(attribute, attributeSyntax) is IUnitInstanceRecord unitInstance)
                {
                    unitInstances.Add(property, unitInstance);
                }
            }
        }

        return unitInstances;
    }

    private sealed class UnitType : IUnitType
    {
        private ITypeSymbol Type { get; }

        private IUnitRecord UnitDefinition { get; }

        private IDisableDocumentationRecord? DisableDocumentation { get; }
        private IEnableDocumentationRecord? EnableDocumentation { get; }

        private IReadOnlyDictionary<IPropertySymbol, IUnitInstanceRecord> UnitInstances { get; }
        private IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> SemanticUnitInstances { get; }

        public UnitType(ITypeSymbol type, IUnitRecord unitDefinition, IDisableDocumentationRecord? disableDocumentation, IEnableDocumentationRecord? enableDocumentation, IReadOnlyDictionary<IPropertySymbol, IUnitInstanceRecord> unitInstances)
        {
            Type = type;

            UnitDefinition = unitDefinition;
            DisableDocumentation = disableDocumentation;
            EnableDocumentation = enableDocumentation;

            UnitInstances = unitInstances;
            SemanticUnitInstances = unitInstances.ToDictionary<KeyValuePair<IPropertySymbol, IUnitInstanceRecord>, IPropertySymbol, ISemanticUnitInstanceRecord>(static (value) => value.Key, static (value) => value.Value, SymbolEqualityComparer.Default);
        }

        ITypeSymbol ISemanticSharpMeasuresType.Type => Type;

        IUnitRecord IUnitType.UnitDefinition => UnitDefinition;
        ISemanticUnitRecord ISemanticUnitType.UnitDefinition => UnitDefinition;

        IDisableDocumentationRecord? ISharpMeasuresType.DisableDocumentation => DisableDocumentation;
        bool ISemanticSharpMeasuresType.DisableDocumentation => DisableDocumentation is not null;

        IEnableDocumentationRecord? ISharpMeasuresType.EnableDocumentation => EnableDocumentation;
        bool ISemanticSharpMeasuresType.EnableDocumentation => EnableDocumentation is not null;

        IReadOnlyDictionary<IPropertySymbol, IUnitInstanceRecord> IUnitType.UnitInstances => UnitInstances;
        IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> ISemanticUnitType.UnitInstances => SemanticUnitInstances;
    }
}
