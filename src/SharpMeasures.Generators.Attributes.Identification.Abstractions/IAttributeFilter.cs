namespace SharpMeasures.Generators.Attributes.Identification;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

/// <summary>Filters collections of attributes by attribute-class.</summary>
public interface IAttributeFilter
{
    /// <summary>Retrieves the first attribute of the specified attribute-class.</summary>
    /// <typeparam name="TAttribute">The attribute-class of the retrieved attribute.</typeparam>
    /// <param name="attributes">The available attributes.</param>
    /// <returns>The first attribute of the specified attribute-class, or <see langword="null"/> if none exists.</returns>
    public abstract AttributeData? GetFirst<TAttribute>(IEnumerable<AttributeData> attributes) where TAttribute : Attribute;

    /// <summary>Retrieves the first attribute of the specified attribute-class.</summary>
    /// <param name="attributeClass">The attribute-class of the retrieved attribute.</param>
    /// <param name="attributes">The available attributes.</param>
    /// <returns>The first attribute of the specified attribute-class, or <see langword="null"/> if none exists.</returns>
    public abstract AttributeData? GetFirst(Type attributeClass, IEnumerable<AttributeData> attributes);

    /// <summary>Retrieves all attributes of the specified attribute-class.</summary>
    /// <typeparam name="TAttribute">The attribute-class of the retrieved attributes.</typeparam>
    /// <param name="attributes">The available attributes.</param>
    /// <returns>All attributes of the specified attribute-class.</returns>
    public abstract IEnumerable<AttributeData> GetAll<TAttribute>(IEnumerable<AttributeData> attributes) where TAttribute : Attribute;

    /// <summary>Retrieves all attribute of the specified attribute-class.</summary>
    /// <param name="attributeClass">The attribute-class of the retrieved attribute.</param>
    /// <param name="attributes">The available attributes.</param>
    /// <returns>All attributes of the specified attribute-class.</returns>
    public abstract IEnumerable<AttributeData> GetAll(Type attributeClass, IEnumerable<AttributeData> attributes);
}
