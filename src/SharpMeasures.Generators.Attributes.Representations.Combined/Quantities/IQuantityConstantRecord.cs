namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="QuantityConstantAttribute"/>.</summary>
public interface IQuantityConstantRecord
{
    /// <summary>Represents syntactic information about the <see cref="QuantityConstantAttribute"/>.</summary>
    public abstract ISyntacticQuantityConstantRecord Syntactic { get; }
}
