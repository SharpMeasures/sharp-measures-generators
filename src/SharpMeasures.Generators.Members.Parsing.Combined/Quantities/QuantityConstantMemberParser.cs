namespace SharpMeasures.Generators.Members.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Identification;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;
using SharpMeasures.Generators.Members.Quantities;

using System;
using System.Threading.Tasks;

/// <inheritdoc cref="IQuantityConstantMemberParser"/>
public sealed class QuantityConstantMemberParser : IQuantityConstantMemberParser
{
    private IQuantityConstantRecordFactory AttributeRecordFactory { get; }
    private IAttributeFilter AttributeFilter { get; }

    /// <summary>Instantiates a <see cref="QuantityConstantMemberParser"/>, parsing members of SharpMeasures quantities as constants.</summary>
    /// <param name="attributeRecordFactory">Creates data records representing the attributes that mark the members as constants.</param>
    /// <param name="attributeFilter">Filters collections of attributes by attribute-class.</param>
    public QuantityConstantMemberParser(IQuantityConstantRecordFactory attributeRecordFactory, IAttributeFilter attributeFilter)
    {
        AttributeRecordFactory = attributeRecordFactory ?? throw new ArgumentNullException(nameof(attributeRecordFactory));
        AttributeFilter = attributeFilter ?? throw new ArgumentNullException(nameof(attributeFilter));
    }

    async Task<IQuantityConstantMember?> IQuantityConstantMemberParser.TryParse(IPropertySymbol property, ITypeSymbol quantityType)
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

        if (await TryExtractPropertySyntax(property).ConfigureAwait(false) is not PropertyDeclarationSyntax propertySyntax)
        {
            return null;
        }

        if (await TryParseAttribute(property).ConfigureAwait(false) is not IQuantityConstantRecord attribute)
        {
            return null;
        }

        SyntacticQuantityConstantMember syntactic = new(propertySyntax, attribute.Syntactic);

        return new QuantityConstantMember(property, syntactic);
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

    private async Task<IQuantityConstantRecord?> TryParseAttribute(IPropertySymbol property)
    {
        if (AttributeFilter.GetFirst<QuantityConstantAttribute>(property.GetAttributes()) is not AttributeData attribute)
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

        return AttributeRecordFactory.Create(attributeSyntax);
    }

    private sealed class QuantityConstantMember : IQuantityConstantMember
    {
        private IPropertySymbol Property { get; }
        private ISyntacticQuantityConstantMember Syntactic { get; }

        public QuantityConstantMember(IPropertySymbol property, ISyntacticQuantityConstantMember syntactic)
        {
            Property = property;
            Syntactic = syntactic;
        }

        IPropertySymbol ISemanticQuantityConstantMember.Property => Property;

        ISyntacticQuantityConstantMember IQuantityConstantMember.Syntactic => Syntactic;
    }

    private sealed class SyntacticQuantityConstantMember : ISyntacticQuantityConstantMember
    {
        private PropertyDeclarationSyntax Property { get; }
        private ISyntacticQuantityConstantRecord Attribute { get; }

        public SyntacticQuantityConstantMember(PropertyDeclarationSyntax property, ISyntacticQuantityConstantRecord attribute)
        {
            Property = property;
            Attribute = attribute;
        }

        PropertyDeclarationSyntax ISyntacticQuantityConstantMember.Property => Property;
        ISyntacticQuantityConstantRecord ISyntacticQuantityConstantMember.Attribute => Attribute;
    }
}
