namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
public interface IVectorGroup
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }
}
