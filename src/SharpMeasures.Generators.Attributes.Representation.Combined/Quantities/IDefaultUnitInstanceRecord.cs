namespace SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents a <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface IDefaultUnitInstanceRecord : ISemanticDefaultUnitInstanceRecord
{
    /// <summary>Represents syntactic information about the <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    public abstract ISyntacticDefaultUnitInstanceRecord Syntactic { get; }
}
