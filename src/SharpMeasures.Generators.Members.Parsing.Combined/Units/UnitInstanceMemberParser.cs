namespace SharpMeasures.Generators.Members.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Identification;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Units;
using SharpMeasures.Generators.Members.Units;

using System;
using System.Threading.Tasks;

/// <inheritdoc cref="IUnitInstanceMemberParser"/>
public sealed class UnitInstanceMemberParser : IUnitInstanceMemberParser
{
    private IUnitInstanceParser AttributeParser { get; }
    private IAttributeFilter AttributeFilter { get; }

    /// <summary>Instantiates a <see cref="UnitInstanceMemberParser"/>, parsing members of SharpMeasures units as unit instances.</summary>
    /// <param name="attributeParser">Parses the attributes that mark members as unit instances.</param>
    /// <param name="attributeFilter">Filters collections of attributes by attribute-class.</param>
    public UnitInstanceMemberParser(IUnitInstanceParser attributeParser, IAttributeFilter attributeFilter)
    {
        AttributeParser = attributeParser ?? throw new ArgumentNullException(nameof(attributeParser));
        AttributeFilter = attributeFilter ?? throw new ArgumentNullException(nameof(attributeFilter));
    }

    async Task<IUnitInstanceMember?> IUnitInstanceMemberParser.TryParse(IPropertySymbol property, ITypeSymbol unitType)
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

        if (await TryExtractPropertySyntax(property) is not PropertyDeclarationSyntax propertySyntax)
        {
            return null;
        }

        if (await TryParseAttribute(property) is not IUnitInstanceRecord attribute)
        {
            return null;
        }

        SyntacticUnitInstanceMember syntactic = new(propertySyntax, attribute.Syntactic);

        return new UnitInstanceMember(property, attribute, syntactic);
    }

    private async Task<PropertyDeclarationSyntax?> TryExtractPropertySyntax(IPropertySymbol property)
    {
        if (property.DeclaringSyntaxReferences.Length is 0)
        {
            return null;
        }

        if (await property.DeclaringSyntaxReferences[0].GetSyntaxAsync().ConfigureAwait(false) is not PropertyDeclarationSyntax syntax)
        {
            return null;
        }

        return syntax;
    }

    private async Task<IUnitInstanceRecord?> TryParseAttribute(IPropertySymbol property)
    {
        if (AttributeFilter.GetFirst<UnitInstanceAttribute>(property.GetAttributes()) is not AttributeData attribute)
        {
            return null;
        }

        if (attribute.ApplicationSyntaxReference is null)
        {
            return null;
        }

        if (await attribute.ApplicationSyntaxReference.GetSyntaxAsync().ConfigureAwait(false) is not AttributeSyntax attributeSyntax)
        {
            return null;
        }

        return AttributeParser.TryParse(attribute, attributeSyntax);
    }

    private sealed class UnitInstanceMember : IUnitInstanceMember
    {
        private IPropertySymbol Property { get; }
        private ISemanticUnitInstanceRecord Attribute { get; }
        private ISyntacticUnitInstanceMember Syntactic { get; }

        public UnitInstanceMember(IPropertySymbol property, ISemanticUnitInstanceRecord attribute, ISyntacticUnitInstanceMember syntactic)
        {
            Property = property;
            Attribute = attribute;
            Syntactic = syntactic;
        }

        IPropertySymbol ISemanticUnitInstanceMember.Property => Property;
        ISemanticUnitInstanceRecord ISemanticUnitInstanceMember.Attribute => Attribute;
        ISyntacticUnitInstanceMember IUnitInstanceMember.Syntactic => Syntactic;
    }

    private sealed class SyntacticUnitInstanceMember : ISyntacticUnitInstanceMember
    {
        private PropertyDeclarationSyntax Property { get; }
        private ISyntacticUnitInstanceRecord Attribute { get; }

        public SyntacticUnitInstanceMember(PropertyDeclarationSyntax property, ISyntacticUnitInstanceRecord attribute)
        {
            Property = property;
            Attribute = attribute;
        }

        PropertyDeclarationSyntax ISyntacticUnitInstanceMember.Property => Property;
        ISyntacticUnitInstanceRecord ISyntacticUnitInstanceMember.Attribute => Attribute;
    }
}
