namespace SharpMeasures.Generators.Members.Units;

/// <summary>Represents an instance of some unit.</summary>
public interface IUnitInstanceMember : ISemanticUnitInstanceMember
{
    /// <summary>Represents syntactic information about the unit instance.</summary>
    public abstract ISyntacticUnitInstanceMember Syntactic { get; }
}
