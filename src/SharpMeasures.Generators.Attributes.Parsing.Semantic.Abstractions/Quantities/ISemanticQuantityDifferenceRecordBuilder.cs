namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="ISemanticQuantityDifferenceRecord"/>.</summary>
public interface ISemanticQuantityDifferenceRecordBuilder : IRecordBuilder<ISemanticQuantityDifferenceRecord>
{
    /// <summary>Specifies the quantity that represents the difference between two instances of the implementing quantity.</summary>
    /// <param name="difference">The quantity that represents the difference between two instances of the implementing quantity.</param>
    public abstract void WithDifference(ITypeSymbol difference);
}
