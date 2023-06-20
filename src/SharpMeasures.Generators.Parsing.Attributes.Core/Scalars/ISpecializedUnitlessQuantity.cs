﻿namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedUnitlessQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}