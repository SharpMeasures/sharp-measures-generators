namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="UnitlessQuantityAttribute"/>.</summary>
public interface IUnitlessQuantityRecord
{
    /// <summary>Represents syntactic information about the <see cref="UnitlessQuantityAttribute"/>.</summary>
    public abstract ISyntacticUnitlessQuantityRecord Syntactic { get; }
}
