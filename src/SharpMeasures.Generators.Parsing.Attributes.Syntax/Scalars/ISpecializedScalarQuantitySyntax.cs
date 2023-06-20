namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedScalarQuantitySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the original scalar quantity, of which this quantity is a specialized form.</summary>
    public abstract Location Original { get; }
}
