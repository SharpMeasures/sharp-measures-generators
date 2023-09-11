namespace SharpMeasures.Generators.Members.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a constant of some quantity.</summary>
public interface ISemanticQuantityConstantMember
{
    /// <summary>The property that defines the constant.</summary>
    public abstract IPropertySymbol Property { get; }
}
