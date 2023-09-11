namespace SharpMeasures.Generators.Types.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Units;

using System.Collections.Generic;

/// <summary>Represents a unit.</summary>
public interface ISemanticUnitType : ISemanticSharpMeasuresType
{
    /// <summary>The <see cref="UnitAttribute{TScalar}"/> that marks the type as a unit.</summary>
    public abstract ISemanticUnitRecord UnitDefinition { get; }

    /// <summary>The unit instances defined by the unit.</summary>
    public abstract IReadOnlyDictionary<IPropertySymbol, ISemanticUnitInstanceRecord> UnitInstances { get; }
}
