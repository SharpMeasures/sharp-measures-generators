namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticSpecializedVectorQuantityRecord"/>.</summary>
public interface ISemanticSpecializedVectorQuantityRecordBuilder : IRecordBuilder<ISemanticSpecializedVectorQuantityRecord>
{
    /// <summary>Specifies the original vector quantity.</summary>
    /// <param name="original">The original vector quantity, of which the quantity is a specialized form.</param>
    public abstract void WithOriginal(ITypeSymbol original);
}
