namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface IQuantitySumRecord : ISemanticQuantitySumRecord
{
    /// <summary>Represents syntactic information about the <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    public abstract ISyntacticQuantitySumRecord Syntactic { get; }
}
