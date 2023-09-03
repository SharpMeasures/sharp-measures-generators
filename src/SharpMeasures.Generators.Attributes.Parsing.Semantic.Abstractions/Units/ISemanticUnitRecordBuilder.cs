namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="ISemanticUnitRecord"/>.</summary>
public interface ISemanticUnitRecordBuilder : IRecordBuilder<ISemanticUnitRecord>
{
    /// <summary>Specifies the scalar quantity primarily described by the unit.</summary>
    /// <param name="scalarQuantity">The scalar quantity primarily described by the unit.</param>
    public abstract void WithScalarQuantity(ITypeSymbol scalarQuantity);

    /// <summary>Specifies whether the unit includes a bias term.</summary>
    /// <param name="biasTerm">Indicates whether the unit includes a bias term.</param>
    public abstract void WithBiasTerm(bool biasTerm);
}
