namespace SharpMeasures.Generators.Attributes.Identification;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IAttributeFilter"/>
public sealed class AttributeFilter : IAttributeFilter
{
    private IAttributeIdentifier Identifier { get; }

    /// <summary>Instantiates a <see cref="AttributeFilter"/>, filtering collections of attributes by attribute-class.</summary>
    /// <param name="identifier">Identifies the attribute-class of attributes.</param>
    public AttributeFilter(IAttributeIdentifier identifier)
    {
        Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
    }

    AttributeData? IAttributeFilter.GetFirst<TAttribute>(IEnumerable<AttributeData> attributes)
    {
        if (attributes is null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        return GetFirst(typeof(TAttribute), attributes);
    }

    AttributeData? IAttributeFilter.GetFirst(Type attributeClass, IEnumerable<AttributeData> attributes)
    {
        if (attributeClass is null)
        {
            throw new ArgumentNullException(nameof(attributeClass));
        }

        if (attributes is null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        return GetFirst(attributeClass, attributes);
    }

    IEnumerable<AttributeData> IAttributeFilter.GetAll<TAttribute>(IEnumerable<AttributeData> attributes)
    {
        if (attributes is null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        return GetAll(typeof(TAttribute), attributes);
    }

    IEnumerable<AttributeData> IAttributeFilter.GetAll(Type attributeClass, IEnumerable<AttributeData> attributes)
    {
        if (attributeClass is null)
        {
            throw new ArgumentNullException(nameof(attributeClass));
        }

        if (attributes is null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        return GetAll(attributeClass, attributes);
    }

    private AttributeData? GetFirst(Type attributeClass, IEnumerable<AttributeData> attributes)
    {
        foreach (var attribute in attributes)
        {
            if (attribute is null)
            {
                throw new ArgumentException($"An {nameof(AttributeData)} in the provided collection was null.", nameof(attributes));
            }

            if (Identifier.IsOfAttributeClass(attributeClass, attribute))
            {
                return attribute;
            }
        }

        return null;
    }

    private IEnumerable<AttributeData> GetAll(Type attributeClass, IEnumerable<AttributeData> attributes)
    {
        foreach (var attribute in attributes)
        {
            if (attribute is null)
            {
                throw new InvalidOperationException($"An {nameof(AttributeData)} in the provided collection was null.");
            }

            if (Identifier.IsOfAttributeClass(attributeClass, attribute))
            {
                yield return attribute;
            }
        }
    }
}
