namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedUnitlessQuantityRecord : ISemanticSpecializedUnitlessQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISyntacticSpecializedUnitlessQuantityRecord Syntactic { get; }
}
