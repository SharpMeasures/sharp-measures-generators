namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISemanticSpecializedUnitlessQuantityRecord"/>.</summary>
public interface ISemanticSpecializedUnitlessQuantityRecordBuilder : IRecordBuilder<ISemanticSpecializedUnitlessQuantityRecord>
{
    /// <summary>Specifies the original quantity.</summary>
    /// <param name="original">The original quantity, of which the quantity is a specialized form.</param>
    public abstract void WithOriginal(ITypeSymbol original);
}
