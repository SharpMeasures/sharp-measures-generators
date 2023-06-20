namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantitySumAttribute{TSum}"/>, with syntactical information.</summary>
public interface ISyntacticQuantitySum : IQuantitySum
{
    /// <summary>Provides syntactical information about the parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    public abstract IQuantitySumSyntax Syntax { get; }
}
