namespace SharpMeasures.Generators.Attributes.Identification;

using Microsoft.CodeAnalysis;

using System;

/// <summary>Allows the attribute-class of attributes to be identified.</summary>
public interface IAttributeIdentifier
{
    /// <summary>Determines whether the provided attribute is of the specified attribute-class.</summary>
    /// <typeparam name="TAttribute">The attribute-class.</typeparam>
    /// <param name="attribute">The attribute.</param>
    /// <returns>A <see cref="bool"/> indicating whether the attribute is of the specified attribute-class.</returns>
    public abstract bool IsOfAttributeClass<TAttribute>(AttributeData attribute) where TAttribute : Attribute;

    /// <summary>Determines whether the provided attribute is of the specified attribute-class.</summary>
    /// <param name="attributeClass">The attribute-class.</param>
    /// <param name="attribute">Tge attribute.</param>
    /// <returns>A <see cref="bool"/> indicating whether the attribute is of the specified attribute-class.</returns>
    public abstract bool IsOfAttributeClass(Type attributeClass, AttributeData attribute);
}
