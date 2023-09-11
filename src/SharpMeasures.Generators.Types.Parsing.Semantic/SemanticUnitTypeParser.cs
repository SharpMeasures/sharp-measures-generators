namespace SharpMeasures.Generators.Types.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Types.Units;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ISemanticUnitTypeParser"/>
public sealed class SemanticUnitTypeParser : ISemanticUnitTypeParser
{
    private ISemanticUnitParser UnitParser { get; }
    private Members.Parsing.Units.ISemanticUnitInstanceMemberParser UnitInstanceParser { get; }

    /// <summary>Instantiates a <see cref="SemanticUnitTypeParser"/>, parsing <see cref="ISemanticUnitType"/>.</summary>
    /// <param name="unitParser">Produces records of <see cref="UnitAttribute{TScalar}"/>.</param>
    /// <param name="unitInstanceParser">Produces records of <see cref="UnitInstanceAttribute"/>.</param>
    public SemanticUnitTypeParser(ISemanticUnitParser unitParser, Members.Parsing.Units.ISemanticUnitInstanceMemberParser unitInstanceParser)
    {
        UnitParser = unitParser ?? throw new ArgumentNullException(nameof(unitParser));
        UnitInstanceParser = unitInstanceParser ?? throw new ArgumentNullException(nameof(unitInstanceParser));
    }

    ISemanticUnitType? ISemanticUnitTypeParser.TryParse(ITypeSymbol type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        var attributes = type.GetAttributes();

        var unitDefinition = ParseUnitDefinition(attributes);

        if (unitDefinition is null)
        {
            return null;
        }

        var disableDocumentation = ParseDisableDocumentation(attributes);
        var enableDocumentation = ParseEnableDocumentation(attributes);

        var unitInstances = ParseUnitInstances(type);

        return new SemanticUnitType(type, unitDefinition, disableDocumentation, enableDocumentation, unitInstances);
    }

    private ISemanticUnitRecord? ParseUnitDefinition(IReadOnlyList<AttributeData> attributes)
    {
        if (AttributeClassMatcher.GetFirst(typeof(UnitAttribute<>), attributes) is not AttributeData attribute)
        {
            return null;
        }

        return UnitParser.TryParse(attribute);
    }

    private bool ParseDisableDocumentation(IReadOnlyList<AttributeData> attributes) => AttributeClassMatcher.GetFirst(typeof(DisableDocumentationAttribute), attributes) is not null;
    private bool ParseEnableDocumentation(IReadOnlyList<AttributeData> attributes) => AttributeClassMatcher.GetFirst(typeof(EnableDocumentationAttribute), attributes) is not null;

    private IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> ParseUnitInstances(ITypeSymbol type)
    {
        Dictionary<IPropertySymbol, ISemanticUnitInstanceRecord> unitInstances = new(SymbolEqualityComparer.Default);

        var members = type.GetMembers();

        foreach (var member in members)
        {
            if (member is IPropertySymbol property && property.IsStatic && property.DeclaredAccessibility == Accessibility.Public && SymbolEqualityComparer.Default.Equals(property.GetMethod?.ReturnType, type))
            {
                if (AttributeClassMatcher.GetFirst(typeof(UnitInstanceAttribute), property.GetAttributes()) is not AttributeData attribute)
                {
                    continue;
                }

                if (UnitInstanceParser.TryParse(attribute) is ISemanticUnitInstanceRecord unitInstance)
                {
                    unitInstances.Add(property, unitInstance);
                }
            }
        }

        return unitInstances;
    }

    private sealed class SemanticUnitType : ISemanticUnitType
    {
        private ITypeSymbol Type { get; }

        private ISemanticUnitRecord UnitDefinition { get; }

        private bool DisableDocumentation { get; }
        private bool EnableDocumentation { get; }

        private IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> UnitInstances { get; }

        public SemanticUnitType(ITypeSymbol type, ISemanticUnitRecord unitDefinition, bool disableDocumentation, bool enableDocumentation, IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> unitInstances)
        {
            Type = type;

            UnitDefinition = unitDefinition;
            DisableDocumentation = disableDocumentation;
            EnableDocumentation = enableDocumentation;

            UnitInstances = unitInstances;
        }

        ITypeSymbol ISemanticSharpMeasuresType.Type => Type;

        ISemanticUnitRecord ISemanticUnitType.UnitDefinition => UnitDefinition;

        bool ISemanticSharpMeasuresType.DisableDocumentation => DisableDocumentation;
        bool ISemanticSharpMeasuresType.EnableDocumentation => EnableDocumentation;

        IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> ISemanticUnitType.UnitInstances => UnitInstances;
    }
}
