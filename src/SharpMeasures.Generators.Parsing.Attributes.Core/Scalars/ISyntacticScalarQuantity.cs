namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>, with syntactical information.</summary>
public interface ISyntacticScalarQuantity : IScalarQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    public abstract IScalarQuantitySyntax Syntax { get; }
}
