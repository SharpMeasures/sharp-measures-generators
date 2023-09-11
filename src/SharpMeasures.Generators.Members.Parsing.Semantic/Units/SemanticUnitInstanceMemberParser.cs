namespace SharpMeasures.Generators.Members.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Identification;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Units;
using SharpMeasures.Generators.Members.Units;

using System;

/// <inheritdoc cref="ISemanticUnitInstanceMemberParser"/>
public sealed class SemanticUnitInstanceMemberParser : ISemanticUnitInstanceMemberParser
{
    private ISemanticUnitInstanceParser AttributeParser { get; }
    private IAttributeFilter AttributeFilter { get; }

    /// <summary>Instantiates a <see cref="SemanticUnitInstanceMemberParser"/>, parsing members of SharpMeasures units as unit instances.</summary>
    /// <param name="attributeParser">Parses the attributes that mark members as unit instances.</param>
    /// <param name="attributeFilter">Filters collections of attributes by attribute-class.</param>
    public SemanticUnitInstanceMemberParser(ISemanticUnitInstanceParser attributeParser, IAttributeFilter attributeFilter)
    {
        AttributeParser = attributeParser ?? throw new ArgumentNullException(nameof(attributeParser));
        AttributeFilter = attributeFilter ?? throw new ArgumentNullException(nameof(attributeFilter));
    }

    ISemanticUnitInstanceMember? ISemanticUnitInstanceMemberParser.TryParse(IPropertySymbol property, ITypeSymbol unitType)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        if (unitType is null)
        {
            throw new ArgumentNullException(nameof(unitType));
        }

        if (property.DeclaredAccessibility != Accessibility.Public)
        {
            return null;
        }

        if (property.IsStatic is false)
        {
            return null;
        }

        if (SymbolEqualityComparer.Default.Equals(property.GetMethod?.ReturnType, unitType) is false)
        {
            return null;
        }

        if (TryParseAttribute(property) is not ISemanticUnitInstanceRecord attribute)
        {
            return null;
        }

        return new SemanticUnitInstanceMember(property, attribute);
    }

    private ISemanticUnitInstanceRecord? TryParseAttribute(IPropertySymbol property)
    {
        if (AttributeFilter.GetFirst<UnitInstanceAttribute>(property.GetAttributes()) is not AttributeData attribute)
        {
            return null;
        }

        return AttributeParser.TryParse(attribute);
    }

    private sealed class SemanticUnitInstanceMember : ISemanticUnitInstanceMember
    {
        private IPropertySymbol Property { get; }
        private ISemanticUnitInstanceRecord Attribute { get; }

        public SemanticUnitInstanceMember(IPropertySymbol property, ISemanticUnitInstanceRecord attribute)
        {
            Property = property;
            Attribute = attribute;
        }

        IPropertySymbol ISemanticUnitInstanceMember.Property => Property;
        ISemanticUnitInstanceRecord ISemanticUnitInstanceMember.Attribute => Attribute;
    }
}
