namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="DisableQuantityDifferenceAttribute"/>.</summary>
public interface IDisableQuantityDifferenceRecord
{
    /// <summary>Represents syntactic information about the <see cref="DisableQuantityDifferenceAttribute"/>.</summary>
    public abstract ISyntacticDisableQuantityDifferenceRecord Syntactic { get; }
}
