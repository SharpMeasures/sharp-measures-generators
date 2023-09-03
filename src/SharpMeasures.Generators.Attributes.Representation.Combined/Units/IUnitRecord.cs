namespace SharpMeasures.Generators.Attributes.Units;

/// <summary>Represents a <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IUnitRecord : ISemanticUnitRecord
{
    /// <summary>Represents syntactic information about the <see cref="UnitAttribute{TScalar}"/>.</summary>
    public abstract ISyntacticUnitRecord Syntactic { get; }
}
