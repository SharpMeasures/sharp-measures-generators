namespace SharpMeasures.Generators.Members.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Represents an instance of some unit.</summary>
public interface ISemanticUnitInstanceMember
{
    /// <summary>The property that defines the unit instance.</summary>
    public abstract IPropertySymbol Property { get; }

    /// <summary>The attribute that marks the member as a unit instance.</summary>
    public abstract ISemanticUnitInstanceRecord Attribute { get; }
}
