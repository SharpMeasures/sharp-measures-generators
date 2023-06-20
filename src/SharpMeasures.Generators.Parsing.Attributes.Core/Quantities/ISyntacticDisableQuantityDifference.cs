namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="DisableQuantityDifferenceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticDisableQuantityDifference : IDisableQuantityDifference
{
    /// <summary>Provides syntactical information about the parsed <see cref="DisableQuantityDifferenceAttribute"/>.</summary>
    public abstract IDisableQuantityDifferenceSyntax Syntax { get; }
}
