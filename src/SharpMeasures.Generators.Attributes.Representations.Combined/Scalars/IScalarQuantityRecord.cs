namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface IScalarQuantityRecord : ISemanticScalarQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    public abstract ISyntacticScalarQuantityRecord Syntactic { get; }
}
