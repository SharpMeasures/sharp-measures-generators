namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>, with syntactical information.</summary>
public interface ISyntacticQuantityDifference : IQuantityDifference
{
    /// <summary>Provides syntactical information about the parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    public abstract IQuantityDifferenceSyntax Syntax { get; }
}
