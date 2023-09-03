namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedScalarQuantityRecord : ISemanticSpecializedScalarQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISyntacticSpecializedScalarQuantityRecord Syntactic { get; }
}
