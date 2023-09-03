namespace SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Represents a <see cref="AllowNegativeAttribute"/>.</summary>
public interface IAllowNegativeRecord
{
    /// <summary>Represents syntactic information about the <see cref="AllowNegativeAttribute"/>.</summary>
    public abstract ISyntacticAllowNegativeRecord Syntactic { get; }
}
