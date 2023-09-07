namespace SharpMeasures.Generators.Attributes.Units;

/// <summary>Represents a <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
public interface IExtendedUnitRecord : ISemanticExtendedUnitRecord
{
    /// <summary>Represents syntactic information about the <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
    public abstract ISyntacticExtendedUnitRecord Syntactic { get; }
}
