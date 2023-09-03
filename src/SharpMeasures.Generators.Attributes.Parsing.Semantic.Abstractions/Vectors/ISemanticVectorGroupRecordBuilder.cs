namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticVectorGroupRecord"/>.</summary>
public interface ISemanticVectorGroupRecordBuilder : IRecordBuilder<ISemanticVectorGroupRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    public abstract void WithUnit(ITypeSymbol unit);
}
