namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantityPropertyAttribute{TResult}"/>, with syntactical information.</summary>
public interface ISyntacticQuantityProperty : IQuantityProperty
{
    /// <summary>Provides syntactical information about the parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    public abstract IQuantityPropertySyntax Syntax { get; }
}
