namespace SharpMeasures.Generators.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents the arguments of a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
public interface ISemanticSpecializedUnitlessQuantityRecord
{
    /// <summary>The original quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
