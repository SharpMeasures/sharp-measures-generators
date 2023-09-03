namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="DisallowNegativeAttribute"/>.</summary>
public interface IDisallowNegativeRecord : ISemanticDisallowNegativeRecord
{
    /// <summary>Represents syntactic information about the <see cref="DisallowNegativeAttribute"/>.</summary>
    public abstract ISyntacticDisallowNegativeRecord Syntactic { get; }
}
