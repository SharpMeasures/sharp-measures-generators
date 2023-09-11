namespace SharpMeasures.Generators.Types;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Units;
using SharpMeasures.Generators.Types.Units;

using System.Collections.Generic;

/// <summary>Represents a unit.</summary>
public interface IUnitType : ISharpMeasuresType, ISemanticUnitType
{
    /// <summary>The <see cref="UnitAttribute{TScalar}"/> that marks the type as a unit.</summary>
    new public abstract IUnitRecord UnitDefinition { get; }

    /// <summary>The unit instances defined by the unit.</summary>
    new public abstract IReadOnlyDictionary<IPropertySymbol, IUnitInstanceRecord> UnitInstances { get; }
}
