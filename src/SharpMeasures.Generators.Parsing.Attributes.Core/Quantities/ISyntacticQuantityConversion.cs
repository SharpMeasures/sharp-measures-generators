namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantityConversionAttribute"/>, with syntactical information.</summary>
public interface ISyntacticQuantityConversion : IQuantityConversion
{
    /// <summary>Provides syntactic information about the parsed <see cref="QuantityConversionAttribute"/>.</summary>
    public abstract IQuantityConversionSyntax Syntax { get; }
}
