namespace SharpMeasures.Generators.Types;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes;
using SharpMeasures.Generators.Attributes.Quantities;
using SharpMeasures.Generators.Attributes.Scalars;

using System.Collections.Generic;

/// <summary>Represents a scalar quantity, defined as a specialized form of another scalar quantity.</summary>
public interface ISemanticSpecializedScalarQuantityType : ISemanticSharpMeasuresType
{
    /// <summary>The <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> that marks the type as a specialized scalar quantity.</summary>
    public abstract ISemanticSpecializedScalarQuantityRecord SpecializedScalarQuantityDefinition { get; }

    /// <summary>The <see cref="QuantityDifferenceAttribute{TDifference}"/> applied to the type.</summary>
    public abstract ISemanticQuantityDifferenceRecord? QuantityDifference { get; }

    /// <summary>The <see cref="QuantitySumAttribute{TSum}"/> applied to the type.</summary>
    public abstract ISemanticQuantitySumRecord? QuantitySum { get; }

    /// <summary>The <see cref="VectorAssociationAttribute{TVector}"/> applied to the type.</summary>
    public abstract ISemanticVectorAssociationRecord? VectorAssociation { get; }

    /// <summary>The <see cref="TypeConversionAttribute"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticTypeConversionRecord> QuantityConversions { get; }

    /// <summary>The <see cref="QuantityOperationAttribute{TResult, TOther}"/> applied to the type.</summary>
    public abstract IReadOnlyList<ISemanticQuantityOperationRecord> QuantityOperations { get; }

    /// <summary>The members of the type that define constants.</summary>
    public abstract IReadOnlyList<ISymbol> Constants { get; }

    /// <summary>The members of the type that define properties.</summary>
    public abstract IReadOnlyList<ISymbol> Properties { get; }

    /// <summary>The members of the type that define processes.</summary>
    public abstract IReadOnlyList<ISymbol> Processes { get; }
}
