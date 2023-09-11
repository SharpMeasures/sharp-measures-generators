namespace SharpMeasures.Generators.Types;

using SharpMeasures.Generators.Attributes;
using SharpMeasures.Generators.Attributes.Quantities;
using SharpMeasures.Generators.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Represents a vector group, defined as a specialized form of another vector group.</summary>
public interface ISemanticSpecializedVectorGroupType
{
    /// <summary>The <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> that marks the type as a specialized vector group.</summary>
    public abstract ISemanticSpecializedVectorGroupRecord SpecializedVectorGroupDefinition { get; }

    /// <summary>The <see cref="QuantityDifferenceAttribute{TDifference}"/> applied to the type.</summary>
    public abstract ISemanticQuantityDifferenceRecord? QuantityDifference { get; }

    /// <summary>The <see cref="QuantitySumAttribute{TSum}"/> applied to the type.</summary>
    public abstract ISemanticQuantitySumRecord? QuantitySum { get; }

    /// <summary>The <see cref="ScalarAssociationAttribute{TScalar}"/> applied to the type.</summary>
    public abstract ISemanticScalarAssociationRecord? ScalarAssociation { get; }

    /// <summary>The <see cref="VectorComponentNamesAttribute"/> applied to the type.</summary>
    public abstract ISemanticVectorComponentNamesRecord? VectorComponentNames { get; }

    /// <summary>The <see cref="TypeConversionAttribute"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticTypeConversionRecord> QuantityConversions { get; }

    /// <summary>The <see cref="QuantityOperationAttribute{TResult, TOther}"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticQuantityOperationRecord> QuantityOperations { get; }
}
