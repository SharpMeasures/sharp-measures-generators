namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="DisableQuantitySumAttribute"/>, with syntactical information.</summary>
public interface ISyntacticDisableQuantitySum : IDisableQuantitySum
{
    /// <summary>Provides syntactical information about the parsed <see cref="DisableQuantitySumAttribute"/>.</summary>
    public abstract IDisableQuantitySumSyntax Syntax { get; }
}
