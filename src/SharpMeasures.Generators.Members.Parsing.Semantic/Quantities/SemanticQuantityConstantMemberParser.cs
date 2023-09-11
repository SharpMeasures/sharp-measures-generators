namespace SharpMeasures.Generators.Members.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Identification;
using SharpMeasures.Generators.Members.Quantities;

using System;

/// <inheritdoc cref="ISemanticQuantityConstantMemberParser"/>
public sealed class SemanticQuantityConstantMemberParser : ISemanticQuantityConstantMemberParser
{
    private IAttributeFilter AttributeFilter { get; }

    /// <summary>Instantiates a <see cref="SemanticQuantityConstantMemberParser"/>, parsing members of SharpMeasures quantities as constants.</summary>
    /// <param name="attributeFilter">Filters collections of attributes by attribute-class.</param>
    public SemanticQuantityConstantMemberParser(IAttributeFilter attributeFilter)
    {
        AttributeFilter = attributeFilter ?? throw new ArgumentNullException(nameof(attributeFilter));
    }

    ISemanticQuantityConstantMember? ISemanticQuantityConstantMemberParser.TryParse(IPropertySymbol property, ITypeSymbol quantityType)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        if (quantityType is null)
        {
            throw new ArgumentNullException(nameof(quantityType));
        }

        if (property.DeclaredAccessibility != Accessibility.Public)
        {
            return null;
        }

        if (property.IsStatic is false)
        {
            return null;
        }

        if (SymbolEqualityComparer.Default.Equals(property.GetMethod?.ReturnType, quantityType) is false)
        {
            return null;
        }

        if (AttributeFilter.GetFirst<QuantityConstantAttribute>(property.GetAttributes()) is null)
        {
            return null;
        }

        return new SemanticQuantityConstantMember(property);
    }

    private sealed class SemanticQuantityConstantMember : ISemanticQuantityConstantMember
    {
        private IPropertySymbol Property { get; }

        public SemanticQuantityConstantMember(IPropertySymbol property)
        {
            Property = property;
        }

        IPropertySymbol ISemanticQuantityConstantMember.Property => Property;
    }
}
