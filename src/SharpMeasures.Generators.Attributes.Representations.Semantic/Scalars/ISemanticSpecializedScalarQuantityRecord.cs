namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
public interface ISemanticSpecializedScalarQuantityRecord
{
    /// <summary>The original scalar quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
