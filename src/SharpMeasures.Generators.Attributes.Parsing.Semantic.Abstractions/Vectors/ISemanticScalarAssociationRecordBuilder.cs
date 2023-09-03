namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticScalarAssociationRecord"/>.</summary>
public interface ISemanticScalarAssociationRecordBuilder : IRecordBuilder<ISemanticScalarAssociationRecord>
{
    /// <summary>Specifies the scalar quantity associated with the vector quantity.</summary>
    /// <param name="scalarQuantity">The scalar quantity associated with the vector quantity.</param>
    public abstract void WithScalarQuantity(ITypeSymbol scalarQuantity);

    /// <summary>Specifies whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    /// <param name="asComponents">Indicates whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</param>
    public abstract void WithAsComponents(bool asComponents);

    /// <summary>Specifies whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</summary>
    /// <param name="asMagnitude">Indicates whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</param>
    public abstract void WithAsMagnitude(bool asMagnitude);
}
