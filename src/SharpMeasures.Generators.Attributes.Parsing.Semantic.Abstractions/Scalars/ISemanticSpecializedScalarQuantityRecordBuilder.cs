namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISemanticSpecializedScalarQuantityRecord"/>.</summary>
public interface ISemanticSpecializedScalarQuantityRecordBuilder : IRecordBuilder<ISemanticSpecializedScalarQuantityRecord>
{
    /// <summary>Specifies the original scalar quantity.</summary>
    /// <param name="original">The original scalar quantity, of which the quantity is a specialized form.</param>
    public abstract void WithOriginal(ITypeSymbol original);
}
