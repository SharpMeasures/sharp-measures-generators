namespace SharpMeasures.Generators.Types;

using SharpMeasures.Generators.Attributes;
using SharpMeasures.Generators.Attributes.Quantities;
using SharpMeasures.Generators.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Represents a vector group.</summary>
public interface ISemanticVectorGroupType
{
    /// <summary>The <see cref="VectorGroupAttribute{TUnit}"/> that marks the type as a vector group.</summary>
    public abstract ISemanticVectorQuantityRecord VectorGroupDefinition { get; }

    /// <summary>Indicate whether the type was marked with a <see cref="DisableQuantityDifferenceAttribute"/>.</summary>
    public abstract bool DisableQuantityDifference { get; }

    /// <summary>Indicates whether the type was marked with a <see cref="DisableQuantitySumAttribute"/>.</summary>
    public abstract bool DisableQuantitySum { get; }

    /// <summary>The <see cref="QuantityDifferenceAttribute{TDifference}"/> applied to the type.</summary>
    public abstract ISemanticQuantityDifferenceRecord? QuantityDifference { get; }

    /// <summary>The <see cref="QuantitySumAttribute{TSum}"/> applied to the type.</summary>
    public abstract ISemanticQuantitySumRecord? QuantitySum { get; }

    /// <summary>The <see cref="ScalarAssociationAttribute{TScalar}"/> applied to the type.</summary>
    public abstract ISemanticScalarAssociationRecord? ScalarAssociation { get; }

    /// <summary>The <see cref="NegativeMagnitudeBehaviourAttribute"/> applied to the type.</summary>
    public abstract ISemanticNegativeMagnitudeBehaviourRecord? NegativeMagnitudeBehaviour { get; }

    /// <summary>The <see cref="VectorComponentNamesAttribute"/> applied to the type.</summary>
    public abstract ISemanticVectorComponentNamesRecord? VectorComponentNames { get; }

    /// <summary>The <see cref="TypeConversionAttribute"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticTypeConversionRecord> QuantityConversions { get; }

    /// <summary>The <see cref="QuantityOperationAttribute{TResult, TOther}"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticQuantityOperationRecord> QuantityOperations { get; }
}
