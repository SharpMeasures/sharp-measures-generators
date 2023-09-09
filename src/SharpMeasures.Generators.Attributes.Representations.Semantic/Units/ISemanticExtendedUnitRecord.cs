namespace SharpMeasures.Generators.Attributes.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents a <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
public interface ISemanticExtendedUnitRecord
{
    /// <summary>The original unit, of which this unit is an extension.</summary>
    public abstract ITypeSymbol Original { get; }
}
