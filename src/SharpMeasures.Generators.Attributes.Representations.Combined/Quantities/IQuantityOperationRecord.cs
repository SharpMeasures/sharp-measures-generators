namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface IQuantityOperationRecord : ISemanticQuantityOperationRecord
{
    /// <summary>Represents syntactic information about the <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    public abstract ISyntacticQuantityOperationRecord Syntactic { get; }
}
