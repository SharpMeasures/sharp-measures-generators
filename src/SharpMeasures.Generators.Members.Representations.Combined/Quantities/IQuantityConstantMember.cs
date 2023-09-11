namespace SharpMeasures.Generators.Members.Quantities;

/// <summary>Represents a constant of a quantity.</summary>
public interface IQuantityConstantMember : ISemanticQuantityConstantMember
{
    /// <summary>Represents syntactic information about the constant.</summary>
    public abstract ISyntacticQuantityConstantMember Syntactic { get; }
}
