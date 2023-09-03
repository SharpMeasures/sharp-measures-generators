namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="QuantityConversionAttribute"/>.</summary>
public interface IQuantityConversionRecord : ISemanticQuantityConversionRecord
{
    /// <summary>Represents syntactic information about the <see cref="QuantityConversionAttribute"/>.</summary>
    public abstract ISyntacticQuantityConversionRecord Syntactic { get; }
}
