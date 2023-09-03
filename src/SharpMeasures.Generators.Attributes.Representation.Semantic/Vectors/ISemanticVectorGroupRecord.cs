﻿namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents the arguments of a <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
public interface ISemanticVectorGroupRecord
{
    /// <summary>The unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }
}