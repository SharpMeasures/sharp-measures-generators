namespace SharpMeasures.Generators.Attributes.Identification;

using Microsoft.CodeAnalysis;

using System;

/// <inheritdoc cref="IAttributeIdentifier"/>
public sealed class AttributeIdentifier : IAttributeIdentifier
{
    bool IAttributeIdentifier.IsOfAttributeClass<TAttribute>(AttributeData attribute)
    {
        if (attribute is null)
        {
            throw new ArgumentNullException(nameof(attribute));
        }

        return IsOfAttributeClass(typeof(TAttribute), attribute);
    }

    bool IAttributeIdentifier.IsOfAttributeClass(Type attributeClass, AttributeData attribute)
    {
        if (attributeClass is null)
        {
            throw new ArgumentNullException(nameof(attributeClass));
        }

        if (attribute is null)
        {
            throw new ArgumentNullException(nameof(attribute));
        }

        return IsOfAttributeClass(attributeClass, attribute);
    }

    private static bool IsOfAttributeClass(Type attributeClass, AttributeData attribute)
    {
        if (attribute.AttributeClass is not INamedTypeSymbol attributeSymbol)
        {
            return false;
        }

        return RepresentsSameType(attributeClass, attributeSymbol);
    }

    private static bool RepresentsSameType(Type runtimeType, ITypeSymbol compileType)
    {
        if (runtimeType.IsGenericType is false)
        {
            return runtimeType.FullName == GetFriendlyName(compileType);
        }

        if (runtimeType.Name != GetFriendlyName(compileType))
        {
            return false;
        }

        if (compileType is not INamedTypeSymbol namedCompileType)
        {
            return false;
        }

        if (runtimeType.ContainsGenericParameters)
        {
            return true;
        }

        for (var i = 0; i < namedCompileType.Arity; i++)
        {
            if (RepresentsSameType(runtimeType.GenericTypeArguments[i], namedCompileType.TypeArguments[i]) is false)
            {
                return false;
            }
        }

        return true;
    }

    private static string GetFriendlyName(ITypeSymbol symbol)
    {
        if (symbol.ContainingNamespace.IsGlobalNamespace)
        {
            return symbol.MetadataName;
        }

        return $"{symbol.ContainingNamespace.Name}.{symbol.MetadataName}";
    }
}
