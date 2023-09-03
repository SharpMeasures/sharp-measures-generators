namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="DisableQuantitySumAttribute"/>.</summary>
public interface IDisableQuantitySumRecord
{
    /// <summary>Represents syntactic information about the <see cref="DisableQuantitySumAttribute"/>.</summary>
    public abstract ISyntacticDisableQuantitySumRecord Syntactic { get; }
}
