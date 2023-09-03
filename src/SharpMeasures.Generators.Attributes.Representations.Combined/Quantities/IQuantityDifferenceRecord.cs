namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface IQuantityDifferenceRecord : ISemanticQuantityDifferenceRecord
{
    /// <summary>Represents syntactic information about the <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    public abstract ISyntacticQuantityDifferenceRecord Syntactic { get; }
}
