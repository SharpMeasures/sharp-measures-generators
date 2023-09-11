namespace SharpMeasures.Generators.Types;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes;
using SharpMeasures.Generators.Attributes.Quantities;
using SharpMeasures.Generators.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Represents a member of a vector group.</summary>
public interface ISemanticVectorGroupMmeberType
{
    /// <summary>The <see cref="VectorGroupMemberAttribute{TGroup}"/> that marks the type as a member of a vector group.</summary>
    public abstract ISemanticVectorGroupMemberRecord VectorGroupMemberDefinition { get; }

    /// <summary>The <see cref="ScalarAssociationAttribute{TScalar}"/> applied to the type.</summary>
    public abstract ISemanticScalarAssociationRecord? ScalarAssociation { get; }

    /// <summary>The <see cref="VectorComponentNamesAttribute"/> applied to the type.</summary>
    public abstract ISemanticVectorComponentNamesRecord? VectorComponentNames { get; }

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
