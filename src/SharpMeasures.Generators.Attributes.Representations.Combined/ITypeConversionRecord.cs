namespace SharpMeasures.Generators.Attributes;

/// <summary>Represents a <see cref="TypeConversionAttribute"/>.</summary>
public interface ITypeConversionRecord : ISemanticTypeConversionRecord
{
    /// <summary>Represents syntactic information about the <see cref="TypeConversionAttribute"/>.</summary>
    public abstract ISyntacticTypeConversionRecord Syntactic { get; }
}
