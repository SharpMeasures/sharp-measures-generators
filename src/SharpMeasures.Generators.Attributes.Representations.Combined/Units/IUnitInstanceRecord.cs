namespace SharpMeasures.Generators.Attributes.Units;

/// <summary>Represents a <see cref="UnitInstanceAttribute"/>.</summary>
public interface IUnitInstanceRecord : ISemanticUnitInstanceRecord
{
    /// <summary>Represents syntactic information about the <see cref="UnitInstanceAttribute"/>.</summary>
    public abstract ISyntacticUnitInstanceRecord Syntactic { get; }
}
