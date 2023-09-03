namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticVectorQuantityRecord"/>.</summary>
public interface ISemanticVectorQuantityRecordBuilder : IRecordBuilder<ISemanticVectorQuantityRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    public abstract void WithUnit(ITypeSymbol unit);

    /// <summary>Specifies the dimension of the vector space.</summary>
    /// <param name="dimension">The dimension of the vector space.</param>
    public abstract void WithDimension(int dimension);
}
