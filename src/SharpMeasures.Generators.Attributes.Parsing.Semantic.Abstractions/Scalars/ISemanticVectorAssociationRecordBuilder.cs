namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISemanticVectorAssociationRecord"/>.</summary>
public interface ISemanticVectorAssociationRecordBuilder : IRecordBuilder<ISemanticVectorAssociationRecord>
{
    /// <summary>Specifies the vector quantity associated with the scalar quantity.</summary>
    /// <param name="vectorQuantity">The vector quantity associated with the scalar quantity.</param>
    public abstract void WithVectorQuantity(ITypeSymbol vectorQuantity);
}
