namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="ISemanticQuantitySumRecord"/>.</summary>
public interface ISemanticQuantitySumRecordBuilder : IRecordBuilder<ISemanticQuantitySumRecord>
{
    /// <summary>Specifies the quantity that represents the sum of two instances of the implementing quantity.</summary>
    /// <param name="sum">The quantity that represents the sum of two instances of the implementing quantity.</param>
    public abstract void WithSum(ITypeSymbol sum);
}
